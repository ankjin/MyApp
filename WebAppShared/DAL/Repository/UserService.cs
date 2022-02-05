using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebAppShared.DAL.Interface;
using WebAppShared.ModelsApi;

namespace WebAppShared.DAL.Repository
{
    public class UserService : IUserService
    {
        public HttpClient _httpClient { get; }
        private readonly AppDataContext _context;
        private readonly IConfiguration _config;

        public UserService(AppDataContext context, IConfiguration config, HttpClient httpClient)
        {
            _context = context;
            _config = config;

            //httpClient.BaseAddress = new Uri(_appSettings.BookStoresBaseAddress);
            httpClient.DefaultRequestHeaders.Add("User-Agent", "BlazorServer");

            _httpClient = httpClient;
        }

        public async Task<UserWithToken> LoginAsync(User user)
        {
            //user.Password = Utility.Encrypt(user.Password);
            //string serializedUser = JsonConvert.SerializeObject(user);

            //var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Users/Login");
            //requestMessage.Content = new StringContent(serializedUser);

            //requestMessage.Content.Headers.ContentType
            //    = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            //var response = await _httpClient.SendAsync(requestMessage);

            //var responseStatusCode = response.StatusCode;
            //var responseBody = await response.Content.ReadAsStringAsync();

            //var returnedUser = JsonConvert.DeserializeObject<User>(responseBody);

            //return await Task.FromResult(returnedUser);




            user.Password = Utility.Encrypt(user.Password);
            user = await _context.Users.Where(u => u.IsActive && u.EmailAddress == user.EmailAddress
                                                && u.Password == user.Password).Include(u => u.Role).FirstOrDefaultAsync();
            //user = await _context.Users.Include(u => u.Role)
            //                            .Where(u => u.IsActive && u.EmailAddress == user.EmailAddress
            //                                    && u.Password == user.Password).FirstOrDefaultAsync();


            if (user != null)
            {
                UserWithToken userWithToken = null;

                RefreshToken refreshToken = GenerateRefreshToken();
                user.RefreshTokens.Add(refreshToken);
                await _context.SaveChangesAsync();

                userWithToken = new UserWithToken(user);
                userWithToken.RefreshToken = refreshToken.Token;

                if (userWithToken == null)
                {
                    return null;
                }

                //sign your token here here..
                userWithToken.AccessToken = GenerateAccessToken(user.Id);
                return userWithToken;
            }
            else
            {
                return null;
            }

        }
        public async Task<UserWithToken> RegisterUserAsync(User user)
        {
            //user.Password = Utility.Encrypt(user.Password);
            //string serializedUser = JsonConvert.SerializeObject(user);

            //var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Users/RegisterUser");
            //requestMessage.Content = new StringContent(serializedUser);

            //requestMessage.Content.Headers.ContentType
            //    = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            //var response = await _httpClient.SendAsync(requestMessage);

            //var responseStatusCode = response.StatusCode;
            //var responseBody = await response.Content.ReadAsStringAsync();

            //var returnedUser = JsonConvert.DeserializeObject<User>(responseBody);

            //return await Task.FromResult(returnedUser);





            user.Password = Utility.Encrypt(user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            //load role for registered user
            user = await _context.Users.Include(u => u.Role)
                                        .Where(u => u.Id == user.Id).FirstOrDefaultAsync();

            UserWithToken userWithToken = null;

            if (user != null)
            {
                RefreshToken refreshToken = GenerateRefreshToken();
                user.RefreshTokens.Add(refreshToken);
                await _context.SaveChangesAsync();

                userWithToken = new UserWithToken(user);
                userWithToken.RefreshToken = refreshToken.Token;
            }

            if (userWithToken == null)
            {
                return null;
            }

            //sign your token here here..
            userWithToken.AccessToken = GenerateAccessToken(user.Id);
            return userWithToken;

        }
        public async Task<User> GetUserByAccessTokenAsync(string accessToken)
        {
            //string serializedRefreshRequest = JsonConvert.SerializeObject(accessToken);

            //var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Users/GetUserByAccessToken");
            //requestMessage.Content = new StringContent(serializedRefreshRequest);

            //requestMessage.Content.Headers.ContentType
            //    = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            //var response = await _httpClient.SendAsync(requestMessage);

            //var responseStatusCode = response.StatusCode;
            //var responseBody = await response.Content.ReadAsStringAsync();

            //var returnedUser = JsonConvert.DeserializeObject<User>(responseBody);

            //return await Task.FromResult(returnedUser);

            User user = await GetUserFromAccessToken(accessToken);

            if (user != null)
            {
                return user;
            }

            return null;
        }
        public async Task<User> RefreshTokenAsync(RefreshRequest refreshRequest)
        {
            string serializedUser = JsonConvert.SerializeObject(refreshRequest);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Users/RefreshToken");
            requestMessage.Content = new StringContent(serializedUser);

            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedUser = JsonConvert.DeserializeObject<User>(responseBody);

            return await Task.FromResult(returnedUser);
        }



        private async Task<User> GetUserFromAccessToken(string accessToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config["JWTSettings:SecretKey"]);

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                SecurityToken securityToken;
                var principle = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out securityToken);

                JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

                if (jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    var userId = principle.FindFirst(ClaimTypes.Name)?.Value;

                    return await _context.Users.Include(u => u.Role)
                                        .Where(u => u.Id.ToString() == userId).FirstOrDefaultAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
            //return new User();
            return null;
        }
        private RefreshToken GenerateRefreshToken()
        {
            RefreshToken refreshToken = new RefreshToken();

            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshToken.Token = Convert.ToBase64String(randomNumber);
            }
            refreshToken.ExpiryDate = DateTime.UtcNow.AddMonths(6);

            return refreshToken;
        }



        private string GenerateAccessToken(Guid userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["JWTSettings:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Convert.ToString(userId))
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }

}

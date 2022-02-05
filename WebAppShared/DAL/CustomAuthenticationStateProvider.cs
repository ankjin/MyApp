using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAppShared.DAL.Interface;
using WebAppShared.ModelsApi;

namespace WebAppShared.DAL
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        public IUserService _userService { get; set; }
        private readonly HttpClient _httpClient;

        public CustomAuthenticationStateProvider(ILocalStorageService localStorage, IUserService userService, HttpClient httpClient)
        {
            _localStorage = localStorage;
            _userService = userService;
            _httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var accessToken = await _localStorage.GetItemAsync<string>("accessToken");

            ClaimsIdentity identity;

            if (accessToken != null && accessToken != string.Empty)
            {
                User user = await _userService.GetUserByAccessTokenAsync(accessToken);
                if (user != null)
                {
                    identity = GetClaimsIdentity(user);
                }
                else
                {
                    await _localStorage.RemoveItemAsync("refreshToken");
                    await _localStorage.RemoveItemAsync("accessToken");
                    await _localStorage.RemoveItemAsync("urole");
                    identity = new ClaimsIdentity();
                }
            }
            else
            {
                identity = new ClaimsIdentity();
            }

            var claimsPrincipal = new ClaimsPrincipal(identity);

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }

        public async Task MarkUserAsAuthenticated(UserWithToken user)
        {
            await _localStorage.SetItemAsync("accessToken", user.AccessToken);
            await _localStorage.SetItemAsync("refreshToken", user.RefreshToken);
            await _localStorage.SetItemAsync("urole", user.Role.RoleName);

            var identity = GetClaimsIdentity(user);

            var claimsPrincipal = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _localStorage.RemoveItemAsync("refreshToken");
            await _localStorage.RemoveItemAsync("accessToken");
            await _localStorage.RemoveItemAsync("urole");

            var identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public async Task<User> GetLoggedInUser()
        {
            var accessToken = await _localStorage.GetItemAsync<string>("accessToken");

            if (accessToken != null && accessToken != string.Empty)
            {
                return await _userService.GetUserByAccessTokenAsync(accessToken);
            }
            return null;
        }

        private ClaimsIdentity GetClaimsIdentity(User user)
        {
            var claimsIdentity = new ClaimsIdentity();

            if (user.EmailAddress != null)
            {
                claimsIdentity = new ClaimsIdentity(new[]
                                {
                                    new Claim(ClaimTypes.Name, user.EmailAddress),
                                    new Claim(ClaimTypes.Role, user.Role.RoleName),
                                    new Claim("IsUserCreatedBefore2020", IsUserEmployedBefore2020(user))
                                }, "apiauth_type");
            }

            return claimsIdentity;
        }

        private string IsUserEmployedBefore2020(User user)
        {
            if (user.CreatedDate.Value.Year < 2020)
                return "true";
            else
                return "false";
        }
    }
}
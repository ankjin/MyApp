using System.Threading.Tasks;
using WebAppShared.ModelsApi;

namespace WebAppShared.DAL.Interface
{
    public interface IUserService
    {
        public Task<UserWithToken> LoginAsync(User user);
        public Task<UserWithToken> RegisterUserAsync(User user);
        public Task<User> GetUserByAccessTokenAsync(string accessToken);
        public Task<User> RefreshTokenAsync(RefreshRequest refreshRequest);
    }
}

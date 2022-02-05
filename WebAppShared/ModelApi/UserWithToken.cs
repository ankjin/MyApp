namespace WebAppShared.ModelsApi
{
    public class UserWithToken : User
    {

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public UserWithToken(User user)
        {
            this.Id = user.Id;
            this.EmailAddress = user.EmailAddress;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.CreatedDate = user.CreatedDate;

            this.Role = user.Role;
        }
    }
}
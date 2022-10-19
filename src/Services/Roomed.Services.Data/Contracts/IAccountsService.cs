namespace Roomed.Services.Data.Contracts
{
    using Microsoft.AspNetCore.Identity;

    public interface IAccountsService<TUser>
        where TUser : IdentityUser, new()
    {
        public Task<IdentityResult> RegisterWithEmailAndUsernameAsync(string email, string username, string password);

        public Task<SignInResult> LoginWithEmailAsync(string email, string password, bool isPersistant = false, bool isLockout = true);

        public Task<SignInResult> LoginWithUsernameAsync(string username, string password, bool isPersistant = false, bool isLockout = true);

        public Task LogoutAsync();

        public bool IsSignedIn(TUser user);

        public Task<TUser> FindUserByEmailAsync(string email);

        public Task<TUser> FindUserByUsernameAsync(string username);

        public Task<TUser> FindUserByIdAsync(string id);

        public TUser CreateUserWithEmailAndUsername(string email, string username);
    }
}

using WalletAppAPI.Model;
namespace WalletAppAPI.Repository
{
    public interface IUserRepository
    {
        Task<bool> RegisterUserAsync(User user);
        Task<User?> LoginUserAsync(UserLogin user);
        Task<User?> GetUserByIdAsync(int id);
    }
}

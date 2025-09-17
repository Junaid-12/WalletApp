using WalletAppAPI.Model;

namespace WalletAppAPI.Repository
{
    public interface IwalletRepository
    {
        Task<bool> AddBalanceAsync(int userId, decimal amount);
        Task<decimal> GetWalletBalanceAsync(int userId);
    }
}

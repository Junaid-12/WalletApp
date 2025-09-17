using WalletAppAPI.Model;
namespace WalletAppAPI.Repository
{
    public interface ITransecationRepository
    {
        Task<bool> SendMoneyAsync(Transaction transaction);
        Task<List<Transaction>> GetUserTransactionsAsync(int userId);
    }
}

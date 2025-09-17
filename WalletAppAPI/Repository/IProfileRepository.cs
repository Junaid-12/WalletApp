using WalletAppAPI.Model;

namespace WalletAppAPI.Repository
{
    public interface IProfileRepository
    {
        Task<Profile> GetProfile(int UserId);
        Task AddProfile(Profile pofile);



    }
}

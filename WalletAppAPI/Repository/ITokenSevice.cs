namespace WalletAppAPI.Repository
{
    public interface ITokenSevice
    {
        string GenrateToken(string username, string Email);
    }
}

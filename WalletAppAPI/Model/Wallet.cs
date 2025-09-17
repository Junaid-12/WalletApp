namespace WalletAppAPI.Model
{
    public class Wallet
    {
        public int Id { get; set; }         // Wallet record ID
        public int UserId { get; set; }     // Foreign key to Users
        public decimal Balance { get; set; }
    }
}

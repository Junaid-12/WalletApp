namespace WalletAppAPI.Model
{
    public class SendMoney
    {

        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public decimal Amount { get; set; }
    }
}

namespace WalletAppAPI.Model
{
    public class Transaction
    {
        public int Id { get; set; }             // Transaction ID
        public int SenderId { get; set; }       // Sender user ID
        public int ReceiverId { get; set; }     // Receiver user ID
        public decimal Amount { get; set; }     // Transfer amount
        public string? TransactionType { get; set; }// Credit/Debit
        public DateTime Timestamp { get; set; } // transection time 
    }
}

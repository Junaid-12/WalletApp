using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WalletAppAPI.Model;

namespace WalletAppAPI.Repository
{
    public class TransecationRepository : ITransecationRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string Con;
        public TransecationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            Con = _configuration.GetConnectionString("DigitalConnection");

        }

      
        public async Task<bool> SendMoneyAsync(Transaction tr)
        {
            using SqlConnection conn = new SqlConnection(Con);
            await conn.OpenAsync();
           
            SqlTransaction transaction = conn.BeginTransaction();

            try
            {
                string chkquery = "Select Balance from Wallets Where UserId =@senderId";
                SqlCommand chkcmd= new SqlCommand(chkquery, conn , transaction);
                chkcmd.Parameters.AddWithValue("@senderId",tr.SenderId);
                object result = await chkcmd.ExecuteScalarAsync();
                if (result == null || result == DBNull.Value) {
                    transaction.Rollback();
                    throw new Exception("Sender Wallets not or Balance no to set");

                }
                decimal senderamount = Convert.ToDecimal(result);
                if (senderamount < tr.Amount) {
                    transaction.Rollback();
                 return false;
                }

                // Deduct from sender
                string query = "UPDATE Wallets SET Balance = Balance - @Amount WHERE UserId = @SenderId";
                SqlCommand deductCmd = new SqlCommand( query,conn, transaction);
                deductCmd.Parameters.AddWithValue("@Amount", tr.Amount);
                deductCmd.Parameters.AddWithValue("@SenderId", tr.SenderId);
                
                await deductCmd.ExecuteNonQueryAsync();
                
                // Add to receiver
                SqlCommand addCmd = new SqlCommand("UPDATE Wallets SET Balance = Balance + @Amount WHERE UserId = @ReceiverId", conn, transaction);
                addCmd.Parameters.AddWithValue("@Amount", tr.Amount);
                addCmd.Parameters.AddWithValue("@ReceiverId", tr.ReceiverId);
                await addCmd.ExecuteNonQueryAsync();
                 
                // Record transaction
                SqlCommand insertCmd = new SqlCommand(@"INSERT INTO Transactions (SenderId, ReceiverId, Amount,TransactionType ,Timestamp) VALUES (@SenderId, @ReceiverId, @Amount,'Credit', @Timestamp),(@SenderId,@ReceiverId, @Amount, 'Debit',@Timestamp);", conn, transaction);
                insertCmd.Parameters.AddWithValue("@SenderId", tr.SenderId);
                insertCmd.Parameters.AddWithValue("@ReceiverId", tr.ReceiverId);
                insertCmd.Parameters.AddWithValue("@Amount", tr.Amount);
                insertCmd.Parameters.AddWithValue("@Timestamp", DateTime.UtcNow);
      
                await insertCmd.ExecuteNonQueryAsync();

                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }

        public async Task<List<Transaction>> GetUserTransactionsAsync(int userId)
        {
            List<Transaction> transactions = new List<Transaction>();

            using SqlConnection conn = new SqlConnection(Con);
            using SqlCommand cmd = new SqlCommand("SELECT *FROM Transactions WHERE     (SenderId = @UserId AND TransactionType = 'Debit')" +
                " OR (ReceiverId = @UserId AND TransactionType = 'Credit')" +
                " ORDER BY Timestamp DESC;",conn);
            cmd.Parameters.AddWithValue("@UserId", userId);
            await conn.OpenAsync();

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                transactions.Add(new Transaction
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    SenderId = Convert.ToInt32(reader["SenderId"]),
                    ReceiverId = Convert.ToInt32(reader["ReceiverId"]),
                    Amount = Convert.ToDecimal(reader["Amount"]),
                    TransactionType = Convert.ToString(reader["TransactionType"]),
                    Timestamp = Convert.ToDateTime(reader["Timestamp"])
                });
            }

            return transactions;
        }

     }


    
}

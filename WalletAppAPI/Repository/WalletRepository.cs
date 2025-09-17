using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WalletAppAPI.Model;



namespace WalletAppAPI.Repository
{

    public class WalletRepository : IwalletRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string Con;
         public WalletRepository(IConfiguration configuration  )
        {
            _configuration = configuration;
            Con = _configuration.GetConnectionString("DigitalConnection");
        }
   
        public async Task<bool> AddBalanceAsync(int userId, decimal amount)
        {
            try
            {

                using SqlConnection conn = new SqlConnection(Con);
                using SqlCommand cmd = new SqlCommand(@"IF EXISTS ( select 1 from Wallets where UserId=@UserId) Update Wallets Set Balance=Balance+@Amount Where UserId=@UserId  ELSE Insert Into Wallets(UserId,Balance) values(@UserId,@Amount)", conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@Amount", amount);
                

                await conn.OpenAsync();
                int result = await cmd.ExecuteNonQueryAsync();
                return result > 0;
            }
            catch (Exception ex) {
                // Log the error for debugging
                Console.WriteLine($"[DB ERROR] {ex.Message}");
                throw; // Pass it to controller
            }
        }

        public async Task<decimal> GetWalletBalanceAsync(int userId)
        {
            using SqlConnection conn = new SqlConnection(Con);
            using SqlCommand cmd = new SqlCommand("SELECT Balance FROM Wallets WHERE UserId = @UserId", conn);

            cmd.Parameters.AddWithValue("@UserId", userId);

            await conn.OpenAsync();
            object? result = await cmd.ExecuteScalarAsync();
            return result != null ? Convert.ToDecimal(result) : 0m;
        }
    }
}

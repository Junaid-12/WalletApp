using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using WalletAppAPI.Model;

namespace WalletAppAPI.Repository
{
    public class UserRepository : IUserRepository
    {
       private readonly IConfiguration _configuration;
        private readonly string con;
        public UserRepository(IConfiguration configuration  )
        {
            _configuration = configuration;
            con = _configuration.GetConnectionString("DigitalConnection");
        }
      
        public async Task<bool> RegisterUserAsync(User user)
        {

           // var haspassword= HashPassword(user.Password);
            using (SqlConnection conn = new SqlConnection(con)) {
            
              await conn.OpenAsync();
                //SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM Users  Where Email=@Email", conn);
                //sqlCommand.Parameters.AddWithValue("Email", user.Email);
                //int exist =(int)( await sqlCommand.ExecuteScalarAsync());
                //if (exist > 0) { return false;  }

                SqlCommand cmd = new SqlCommand(@"Insert Into Users (Username,Email,Password) values(@Username,@Email,@Password)", conn);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Email", user.Email);       
                cmd.Parameters.AddWithValue("@Password", user.Password);
                return await cmd.ExecuteNonQueryAsync()>0;
            
            }

        }
        public async Task<User?> GetUserByIdAsync(int id)
        {
            using SqlConnection conn = new SqlConnection(con);
            using SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Id = @Id", conn);

            cmd.Parameters.AddWithValue("@Id", id);
            await conn.OpenAsync();
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new User
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Username = reader["Username"].ToString()!,
                    Email = reader["Email"].ToString()!,
                    Password = reader["PasswordHash"].ToString()!
                };
            }

            return null;
        }
    

         public async Task<User?> LoginUserAsync(UserLogin user)
        {

        using SqlConnection conn = new SqlConnection(con);
        using SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Email = @Email AND Username = @Username", conn);

        cmd.Parameters.AddWithValue("@Email", user.Email);
        cmd.Parameters.AddWithValue("@Username", user.Username); // Hash this before calling

        await conn.OpenAsync();
        using SqlDataReader reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return new User
            {
                Id = Convert.ToInt32(reader["Id"]),
                Username = reader["Username"].ToString()!,
                Email = reader["Email"].ToString()!,
                Password = reader["Password"].ToString()!
            };
        }

        return null;
    }


    
        //private string HashPassword(string raw)
        //{
        //    using (SHA256 sha = SHA256.Create())
        //    {
        //        byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(raw));
        //        return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        //    }



        //}
    }
}

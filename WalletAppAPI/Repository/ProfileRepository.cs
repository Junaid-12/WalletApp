using Microsoft.Data.SqlClient;
using NLog.Layouts;
using WalletAppAPI.Model;

namespace WalletAppAPI.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly string con;
        private readonly  IWebHostEnvironment _env;
        public ProfileRepository(IConfiguration cong , IWebHostEnvironment env)
        {
            con = cong.GetConnectionString("DigitalConnection");
            _env= env;
        }
        public async Task<Profile> GetProfile(int UserId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    string query = " Select UserId, Name,PhoneNo,Address,Image from Profiles where UserId=@UserId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    conn.Open();
                    using SqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader.Read())
                    {
                        return new Profile
                        {
                            
                            UserId = Convert.ToInt32(reader["UserId"]),
                            Name = reader["Name"].ToString(),
                            PhoneNo = reader["PhoneNo"].ToString(),
                            Address = reader["Address"].ToString(),
                            Image = reader["Image"].ToString(),
                        };

                    }
                    return null;
                }
            }
            
            catch (Exception ex) { return null; }
        }
        public async Task AddProfile(Profile profile)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(con))
                {
                    string query = @" if Exists (Select 1 from Profiles Where UserId=@UserId) Update Profiles set Name=@Name , PhoneNo=@PhoneNo,Address=@Address,Image=@Image   Where UserId=@UserId   
                         else   Insert Into Profiles (Name,UserId,PhoneNo,Address,Image) values(@Name,@UserId,@PhoneNo,@Address,@Image) ";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    
                    cmd.Parameters.AddWithValue("@UserId", profile.UserId);
                    cmd.Parameters.AddWithValue("@Name", profile.Name);
                    cmd.Parameters.AddWithValue("@PhoneNo", profile.PhoneNo);
                    cmd.Parameters.AddWithValue("@Address", profile.Address);
                    cmd.Parameters.AddWithValue("@Image", profile.Image);
                    connection.Open();
                    await cmd.ExecuteNonQueryAsync();
                }
            
            } catch( Exception ex) {}
        }
        public async Task<string> SaveImage(IFormFile file)
        {
            var Imagefolder = Path.Combine(_env.WebRootPath, "Uploads");
            if (!Directory.Exists(Imagefolder)) {
               Directory.CreateDirectory(Imagefolder);
            }
            var filename=Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
            var filepath=Path.Combine(Imagefolder, filename);
            using (var stream = new FileStream(filepath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            };

            return filename;
        }
        
    }
}

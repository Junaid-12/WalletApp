namespace WalletAppAPI.Model
{
    public class User
    {
        public int Id { get; set; }           // Auto-generated user ID
        public string Username { get; set; }  // Login/display name
        public string Email { get; set; }     // Used for login
        public string Password { get; set; }
    }
}

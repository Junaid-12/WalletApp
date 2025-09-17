namespace WalletAppAPI.Model
{
    public class Profile
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }   
    }
    public class ProfileUpload
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
       
        public IFormFile ImageFile { get; set; }
    }
}

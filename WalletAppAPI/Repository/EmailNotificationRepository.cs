using Microsoft.Extensions.Options;
using System.Net;
using WalletAppAPI.Model;
using Microsoft.Data.SqlClient;
using System.Net.Mail;



namespace WalletAppAPI.Repository
{
    public class EmailNotificationRepository
    {
        private readonly EmailNotification _settings;
        private readonly string Con;

        public EmailNotificationRepository(IOptions<EmailNotification> settings, IConfiguration _configuration)
        {
            _settings = settings.Value;
            Con = _configuration.GetConnectionString("DigitalConnection");
        }

        public async Task<bool> ReciveEmailUser(Transaction user)
        {
            try
            {
                string reciverName = "";
                string reciverEmail = "";
                string senderName = "";
                string SenderEmail = "";
                using (SqlConnection conn = new SqlConnection(Con))
                {
                    string query = @"
        SELECT 
            s.Username AS SenderName, 
            s.Email AS SenderEmail,
            r.Username AS ReceiverName,
            r.Email AS ReceiverEmail
        FROM Users s
        JOIN Users r ON r.Id = @ReceiverId
        WHERE s.Id = @SenderId";

                    SqlCommand sqlCommand = new SqlCommand(query, conn);
                    sqlCommand.Parameters.AddWithValue("ReceiverId", user.ReceiverId);
                    sqlCommand.Parameters.AddWithValue("SenderId", user.SenderId);
                    conn.Open();
                    SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();
                    if (reader.Read())
                    {
                        reciverName = reader["ReceiverName"].ToString();
                        reciverEmail = reader["ReceiverEmail"].ToString();
                        senderName = reader["SenderName"].ToString();
                        SenderEmail = reader["SenderEmail"].ToString();

                    }
                }
              
               
                
                string subject = "You Reviced Money!";
                string body = $"<h3> Dear Mr {reciverName}</h3>" + $"<p> Conguralations!! You Received money  <b>{user.Amount} from User The Email Id is {SenderEmail}  {senderName}On  {DateTime.Now}</p>";
                await SendEmailAsync(reciverEmail, subject, body,SenderEmail);
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email sending failed: {ex.Message}");
                return false;
            }
        }
        public async Task SendEmailAsync(string toEmail, string subject, string body, string senderEmail)
        {
            try
            {
                using (var client = new SmtpClient(_settings.SmtpServer, _settings.Port))
                {
                    client.Credentials = new NetworkCredential(_settings.Username, _settings.Password);
                    client.EnableSsl = true;

                    var mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(senderEmail, _settings.SenderName);
                    mailMessage.To.Add(toEmail);
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    await client.SendMailAsync(mailMessage);
                }

            }
            catch
            {

            }

        }
    }
}

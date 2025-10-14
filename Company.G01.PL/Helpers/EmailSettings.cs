using System.Net;
using System.Net.Mail;

namespace Company.G01.PL.Helpers
{
    public static class EmailSettings
    {
        public static bool SendEmail(Email email)
        {
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("medhatmoaz52@gmail.com", "hxdmxxkgvzznodzy");
                //hxdmxxkgvzznodzy
                client.Send("medhatmoaz52@gmail.com", email.To, email.Subject, email.Body);


            return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

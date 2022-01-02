using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
//helper class to send the email using parameters passed.

namespace NEAproject.Models
{
    public class EmailSender
    {
        public static bool sendtestemail(string emailaddress, string message,string subject
            )
            //static method to send the email depending on parameters passed 
        {
            SmtpClient smtpserver = new SmtpClient();
            //creates a new object of smtp client to send the email 
            smtpserver.Host = "smtp.gmail.com";
            smtpserver.Port= 587;
            smtpserver.EnableSsl = true;
            smtpserver.UseDefaultCredentials = false;
            smtpserver.Credentials = new NetworkCredential("belh2808", "Privet123!"); //replace later
            smtpserver.DeliveryMethod = SmtpDeliveryMethod.Network;
            var mail = new MailMessage();
            //exception handling 
            try
            {
                mail.From = new MailAddress("belh2808@gmail.com", "arabella hurrell", System.Text.Encoding.UTF8);
                mail.To.Add(emailaddress);
                mail.Subject = subject;
                mail.Body = message;
                smtpserver.Send(mail);
                //actually sends the email 
                return true;
            }
            catch(Exception ex)
            {
                //returns false if anything goes wrong 
                return false;
            }

        }
    }
}

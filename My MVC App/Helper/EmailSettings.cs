using Demo.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace My_MVC_App.Helper
{
	public class EmailSettings
	{
		public static void SendEmail(Email email)
		{
			//mail server : gmail smtp client

			var client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;

			client.Credentials = new NetworkCredential("youssefahmedsaidmohamed@gmail.com", "khsn pdsy kxpo usgj");

			client.Send("youssefahmedsaidmohamed@gmail.com", email.Recipient, email.Subject, email.Body);
		}
	}
}

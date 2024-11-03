using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;


public class NotificationService : INotificationService
{

  private readonly string _smtpHost;
  private readonly int _smptPort;
  private readonly string _fromEmail;
  private readonly string _fromEmailPassword;

  public NotificationService(IConfiguration configuration)
  {
    _smtpHost = configuration["SmtpSettings:Host"];
    _smptPort = int.Parse(configuration["SmtpSettings:Port"]);
    _fromEmail = configuration["SmtpSettings:FromEmail"];
    _fromEmailPassword = configuration["SmtpSettings:FromEmailPassword"];

  }
  public void SendNotification(string message)
  {
    Console.WriteLine($"Notification sent: {message}");
    SendEmail("Employee Management System", message);
  }

  private void SendEmail(string subject, string body)
  {
    using var client = new SmtpClient(_smtpHost, _smptPort);
    client.Credentials = new NetworkCredential(_fromEmail, _fromEmailPassword);
    client.EnableSsl = true;

    var mailMessage = new MailMessage
    {
      From = new MailAddress(_fromEmail),
      Subject = subject,
      Body = body,
      IsBodyHtml = false,
    };
    mailMessage.To.Add("aarif14@hotmail.com"); // replace with your recipient email address

    try
    {
      client.Send(mailMessage);
      Console.WriteLine("Email sent successfully.");
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error sending email: {ex.Message}");
    }
  }
}

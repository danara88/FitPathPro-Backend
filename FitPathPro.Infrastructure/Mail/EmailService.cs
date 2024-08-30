using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace FitPathPro.Infrastructure.Mail;

/// <summary>
/// Service in charge to send emails
/// </summary>
public class EmailService : IEmailSender
{
    private readonly SmtpSettings _smtpSettings;

    public EmailService(IOptions<SmtpSettings> smtpSettings)
    {
        _smtpSettings = smtpSettings.Value;
    }

    /// <summary>
    /// Method to send email
    /// </summary>
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        try
        {
            var message = new MimeMessage();

            // From
            message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));

            // To
            message.To.Add(new MailboxAddress("", email));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = htmlMessage };

            // Open smtp client and send email
            using (var client = new SmtpClient())
            {
                // StartTls: Inform to mail server that the content must be encrypted
                // Use 587 port to send encrypted mail
                await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port,  SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_smtpSettings.UserName, _smtpSettings.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
        catch (Exception)
        {
            throw new Exception();
        }
    }
}


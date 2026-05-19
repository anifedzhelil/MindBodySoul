using brevo_csharp.Api;
using brevo_csharp.Model;
using MindBodySoul.Services.Interface;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using Configuration = brevo_csharp.Client.Configuration;

namespace MindBodySoul.Services.Implementation;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
   
    public async Task SendEmailAsync(string toEmail, string toName, string subject, string htmlContent)
    {
        Configuration.Default.ApiKey["api-key"] = _configuration["Brevo:ApiKey"];

        var apiInstance = new TransactionalEmailsApi();

        var sendSmtpEmail = new SendSmtpEmail(
            sender: new SendSmtpEmailSender(
                name: _configuration["Brevo:SenderName"],
                email: _configuration["Brevo:SenderEmail"]
            ),
            to: new List<SendSmtpEmailTo>
            {
                new SendSmtpEmailTo(email: toEmail, name: toName)
            },
            subject: subject,
            htmlContent: htmlContent
        );

        await apiInstance.SendTransacEmailAsync(sendSmtpEmail);
    }
}
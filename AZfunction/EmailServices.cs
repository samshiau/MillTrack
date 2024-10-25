using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EmailFunctionApp
{
    public static class SendEmailFunction
    {
        [FunctionName("SendEmailFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function to send email using Gmail SMTP.");

            // Parse the request to get the email content
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            string toEmail = req.Query["toEmail"];
            string subject = req.Query["subject"];
            string body = req.Query["body"];

            try
            {
                // Gmail SMTP server configuration
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587, // TLS port
                    Credentials = new NetworkCredential(Environment.GetEnvironmentVariable("EmailUsername"), Environment.GetEnvironmentVariable("EmailPassword")),
                    EnableSsl = true,
                };

                // Create the email message
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("milltrack017@gmail.com"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false,
                };
                mailMessage.To.Add(toEmail);

                // Send the email
                await smtpClient.SendMailAsync(mailMessage);

                return new OkObjectResult($"Email sent successfully to {toEmail}");
            }
            catch (Exception ex)
            {
                log.LogError($"Failed to send email: {ex.Message}");
                return new BadRequestObjectResult("Failed to send email");
            }
        }
    }
}

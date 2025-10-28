using Microsoft.AspNetCore.Mvc;
using Site.UI.Models;
using System.Net;
using System.Net.Mail;

namespace Site.UI.Controllers
{
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private readonly IConfiguration _config;

        public ContactController(ILogger<ContactController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ContactFormModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                // 🔹 appsettings.json'dan SMTP bilgilerini al
                var emailSettings = _config.GetSection("EmailSettings");
                string fromEmail = emailSettings["FromEmail"]!;
                string password = emailSettings["Password"]!;
                string toEmail = emailSettings["ToEmail"]!;
                string smtpServer = emailSettings["SmtpServer"]!;
                int port = int.Parse(emailSettings["Port"]!);

                // 🔹 Mail içeriği
                var message = new MailMessage();
                message.From = new MailAddress(fromEmail);
                message.To.Add(toEmail);
                message.Subject = $"📩 New Message: {model.Subject}";
                message.Body = $@"
                    Name: {model.Name}
                    Email: {model.Email}
                    Subject: {model.Subject}
                    Message:
                    {model.Message}
                ";
                message.IsBodyHtml = false;

                // 🔹 SMTP gönderimi
                using (var client = new SmtpClient(smtpServer, port))
                {
                    client.Credentials = new NetworkCredential(fromEmail, password);
                    client.EnableSsl = true;
                    client.Send(message);
                }

                ViewBag.Success = "✅ Your message has been sent successfully!";
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending contact email");
                ViewBag.Error = "❌ There was a problem sending your message. Please try again later.";
            }

            return View();
        }
    }
}

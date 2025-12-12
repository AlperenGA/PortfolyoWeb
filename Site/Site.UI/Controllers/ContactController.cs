using Microsoft.AspNetCore.Mvc;
using Site.UI.Models;
using System.Net.Mail;

namespace Site.UI.Controllers
{
    public class ContactController : Controller
    {
        private readonly SmtpClient _smtpClient;

        public ContactController(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }

        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ContactFormModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var message = new MailMessage
                {
                    From = new MailAddress("seninmailin@gmail.com"),
                    Subject = $"📩 New Message: {model.Subject}",
                    Body = $@"
                        Name: {model.Name}
                        Email: {model.Email}
                        Subject: {model.Subject}
                        Message:
                        {model.Message}
                    ",
                    IsBodyHtml = false
                };
                message.To.Add("alici@gmail.com");

                _smtpClient.Send(message);

                ViewBag.Success = "✅ Your message has been sent successfully!";
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"❌ Error sending email: {ex.Message}";
            }

            return View();
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Site.UI.Models
{
    public class ContactFormModel
    {
        [Required(ErrorMessage = "Please enter your name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please enter a subject")]
        public string? Subject { get; set; }

        [Required(ErrorMessage = "Please enter your message")]
        public string? Message { get; set; }
    }
}

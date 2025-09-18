using System.ComponentModel.DataAnnotations;

namespace ThreeLayerProject.UI.Models
{
    public class ContactViewModel
{
    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, StringLength(150)]
    public string Subject { get; set; } = string.Empty;

    [Required, StringLength(1000)]
    public string Message { get; set; } = string.Empty;
}

}

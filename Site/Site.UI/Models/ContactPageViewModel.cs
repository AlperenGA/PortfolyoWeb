using ThreeLayerProject.Entities.Models;

namespace Site.UI.Models
{
    public class ContactPageViewModel
    {
        // Admin panelinden girilen bilgileri göstermek için
        public ContactInfo? ContactInfo { get; set; }

        // Formdan gelen veriyi tutmak için
        public ContactMessage ContactMessage { get; set; } = new ContactMessage();
    }
}
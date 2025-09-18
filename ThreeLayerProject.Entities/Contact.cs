namespace ThreeLayerProject.Entities
{
    public class ContactMessage
    {
        public int Id { get; set; }             // Primary Key
        public string Name { get; set; }        // Form -> name
        public string Email { get; set; }       // Form -> email
        public string Subject { get; set; }     // Form -> subject
        public string Message { get; set; }     // Form -> message
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // kayıt zamanı
    }
}

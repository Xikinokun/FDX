namespace FDXTestApp.API.Models
{
    public class RecipientModel
    {
        public Guid Id { get; set; }
        public string Phone { get; set; } = null!;
        public string DeliveryStatus { get; set; } = null!;
    }
}

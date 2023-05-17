using FDXTestApp.Domain.Enums;

namespace FDXTestApp.Domain.Entities
{
    public class Recipient
    {
        public Guid Id { get; set; }
        public string Phone { get; set; } = null!;
        public DeliveryEnum DeliveryStatus { get; set; }
        public Guid SmsId { get; set; }
        public Sms Sms { get; set; } = null!;
    }
}

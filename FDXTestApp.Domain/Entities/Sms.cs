using FDXTestApp.Domain.Enums;

namespace FDXTestApp.Domain.Entities
{
    public class Sms
    {
        public Guid Id { get; set; }
        public string From { get; set; } = null!;
        public string Content { get; set; } = null!;
        public StatusEnum Status { get; set; }
        public ICollection<Recipient> To { get; set; } = null!;
    }
}

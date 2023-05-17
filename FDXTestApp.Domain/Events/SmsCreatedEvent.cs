using FDXTestApp.Domain.Enums;

namespace FDXTestApp.Domain.Events
{
    public class SmsCreatedEvent : Event
    {
        public Guid Id { get; set; }
        public string From { get; set; } = null!;
        public string[] To { get; set; } = null!;
        public string Content { get; set; } = null!;
        public StatusEnum Status { get; set; }
    }
}

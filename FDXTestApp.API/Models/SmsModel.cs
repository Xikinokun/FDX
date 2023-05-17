namespace FDXTestApp.API.Models
{
    public class SmsModel
    {
        public Guid Id { get; set; }
        public string From { get; set; } = null!;
        public string[] To { get; set; } = null!;
        public string Content { get; set; } = null!;
    }
}

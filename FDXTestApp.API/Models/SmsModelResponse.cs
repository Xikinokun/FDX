namespace FDXTestApp.API.Models
{
    public class SmsModelResponse
    {
        public Guid Id { get; set; }
        public string From { get; set; } = null!;
        public ICollection<RecipientModel> To { get; set; } = null!;
        public string Content { get; set; } = null!;
    }
}

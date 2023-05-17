namespace FDXTestApp.Application.ConfigurationSettings
{
    public class RabbitMqSettings
    {
        public string Host { get; set; } = null!;
        public string QueueName { get; set; } = null!;
        public string VirtualHost { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}

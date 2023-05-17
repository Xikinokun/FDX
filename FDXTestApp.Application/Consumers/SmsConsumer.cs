using FDXTestApp.Application.Handlers;
using FDXTestApp.Domain.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FDXTestApp.Application.Consumers
{
    public class SmsConsumer : IConsumer<SmsCreatedEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SmsConsumer> _logger;

        public SmsConsumer(IMediator mediator, ILogger<SmsConsumer> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<SmsCreatedEvent> context)
        {
            if (context?.Message == null)
            {
                _logger.LogWarning("sms event is null");
                return;
            }

            var smsEvent = context.Message;

            var command = new SmsCreatedCommand(smsEvent.Id, smsEvent.From, smsEvent.To, smsEvent.Content, smsEvent.Status);

            await _mediator.Send(command);
        }
    }
}

using FDXTestApp.Domain.Entities;
using FDXTestApp.Domain.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FDXTestApp.Application.Handlers
{
    internal class SmsCommandHandler : IRequestHandler<SmsCommand, Unit>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<SmsCommandHandler> _logger;

        public SmsCommandHandler(IPublishEndpoint publishEndpoint, ILogger<SmsCommandHandler> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task<Unit> Handle(SmsCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                _logger.LogError("No data from request");
                return Unit.Value;
            }

            var sms = new SmsCreatedEvent
            {
                Id = request!.Id,
                From = request.From,
                To = request.To,
                Content = request.Content,
                Status = request.Status,
            };

            await _publishEndpoint.Publish(sms, cancellationToken);
            _logger.LogInformation("Message has been published");
            return Unit.Value;
        }
    }
}

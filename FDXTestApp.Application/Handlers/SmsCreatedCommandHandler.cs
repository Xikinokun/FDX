using FDXTestApp.Application.Contracts;
using FDXTestApp.Domain.Entities;
using FDXTestApp.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace FDXTestApp.Application.Handlers
{
    internal class SmsCreatedCommandHandler : IRequestHandler<SmsCreatedCommand>
    {
        private readonly ISmsRepository _smsRepository;
        private readonly ILogger<SmsCreatedCommandHandler> _logger;

        public SmsCreatedCommandHandler(ISmsRepository smsRepository, ILogger<SmsCreatedCommandHandler> logger)
        {
            _smsRepository = smsRepository;
            _logger = logger;
        }

        public async Task Handle(SmsCreatedCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Receiving message");

            if (request == null)
            {
                _logger.LogWarning("Sms message is null");
                return;
            }

            var sms = new Sms
            {
                Id = request.Id,
                From = request.From,
                To = request.To.Select(x => new Recipient
                    {
                        Id = Guid.NewGuid(),
                        SmsId = request.Id,
                        Phone = x,
                        DeliveryStatus = CheckPhone(x) ? DeliveryEnum.Delivered : DeliveryEnum.Failed
                    }).ToList(),
                Content = request.Content,
                Status = StatusEnum.Processed
            };

            _logger.LogInformation("Received message");

            await _smsRepository.AddSmsAsync(sms, cancellationToken);
        }

        private bool CheckPhone(string phone) 
        {
            string pattern = @"^\d{10}$";

            if (Regex.IsMatch(phone, pattern))
            {
                _logger.LogInformation($"Phone: {phone} is valid");
                return true;
            }
            _logger.LogInformation($"Phone: {phone} is invalid");
            return false;
        }
    }
}

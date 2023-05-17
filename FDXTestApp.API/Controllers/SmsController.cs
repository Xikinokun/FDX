using FDXTestApp.API.Models;
using FDXTestApp.Application.Handlers;
using FDXTestApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FDXTestApp.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SmsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SmsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create SMS with specific sms's data
        /// </summary>
        /// <remarks>
        /// Samples request:
        ///     POST /api/v1/sms/create
        ///     {
        ///         "id": eadb09f4-5eda-477b-a065-2fc9c356a9b7,
        ///         "from": "222",
        ///         "to": ["111", "333"]
        ///         "content": "content"
        ///     }
        /// </remarks>
        /// <param name="smsModel">sms model</param>
        /// <response code="200">Sms has been created</response>
        /// <response code="400">Sms's data empty</response>
        [HttpPost("create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateSms([FromBody] SmsModel smsModel)
        {
            if (smsModel == null)
            {
                return BadRequest();
            }

            var command = new SmsCommand(smsModel.Id, smsModel.From, smsModel.To, smsModel.Content, Domain.Enums.StatusEnum.Processing);

            await _mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Gets sms information
        /// </summary>
        /// <remarks>
        /// Samples request:
        ///     GET /api/v1/sms?id=b4ffe073-94ba-4183-87ae-a95b00c62cfd
        /// </remarks>
        /// <param name="id">sms id</param>
        /// <response code="200">Returns sms</response>
        /// <response code="400">Sms not found</response>
        [HttpGet("sms")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetSms([FromQuery] Guid id)
        {
            var sms = await _mediator.Send(new GetSmsQuery
            {
                Id = id
            });

            if (sms == null)
            {
                return BadRequest("Sms not found");
            }

            var result = new SmsModelResponse
            {
                Id = sms.Id,
                From = sms.From,
                To = sms.To.Select(r => new RecipientModel
                {
                    Id = r.Id,
                    Phone = r.Phone,
                    DeliveryStatus = r.DeliveryStatus.ToString()
                }).ToList(),
                Content = sms.Content
            };

            return Ok(result);
        }

        /// <summary>
        /// Gets all sms information
        /// </summary>
        /// <remarks>
        /// Samples request:
        ///     GET /api/v1/allSms
        /// </remarks>
        /// <response code="200">Returns all sms</response>
        /// <response code="400">Sms not found</response>
        [HttpGet("allSms")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetSms()
        {
            var smsList = await _mediator.Send(new GetSmsListQuery());

            if (smsList == null)
            {
                return BadRequest("Sms not found");
            }

            var result = smsList.Select(sms => new SmsModelResponse
            {
                Id = sms.Id,
                From = sms.From,
                To = sms.To.Select(r => new RecipientModel
                {
                    Id = r.Id,
                    Phone = r.Phone,
                    DeliveryStatus = r.DeliveryStatus.ToString()
                }).ToList(),
                Content = sms.Content
            });

            return Ok(result);
        }
    }
}

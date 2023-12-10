using FluentValidation;
using MarkupProcessor.Application.Dto;
using MarkupProcessor.Commands;
using MarkupProcessor.Data.Models;
using MarkupProcessor.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MarkupProcessor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowDiagramController : ControllerBase
    {
        private readonly ILogger<FlowDiagramController> _logger;
        private readonly IMediator _mediator;
        private readonly IValidator<FlowDiagram> _validator;

        public FlowDiagramController(ILogger<FlowDiagramController> logger, IMediator mediator, IValidator<FlowDiagram> validator) 
        {
            _logger = logger;
            _mediator = mediator;
            _validator = validator;
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new FlowDiagramQuery());
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Post(FlowDiagram flowDiagram)
        {
            try
            {
                var validateObject = await _validator.ValidateAsync(flowDiagram);
                if (!validateObject.IsValid)
                    return BadRequest(validateObject.Errors);

                var result = await _mediator.Send(new FlowDiagramInformationCommand(flowDiagram));
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an issue with the request");
                return BadRequest(ex.Message);
            }
        }
    }
}

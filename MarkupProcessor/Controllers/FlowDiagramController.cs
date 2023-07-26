using MarkupProcessor.Application.Dto;
using MarkupProcessor.Commands;
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

        public FlowDiagramController(ILogger<FlowDiagramController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Post(FlowDiagramDto flowDiagram)
        {
            try
            {
                var result = await _mediator.Send(new FlowDiagramInformationCommand
                {
                    FlowDiagramInformationDto = flowDiagram
                });
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

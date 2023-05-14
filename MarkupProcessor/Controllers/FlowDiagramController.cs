using MarkupProcessor.Application.Commands;
using MarkupProcessor.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;
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
                var flowDiagramInfo = new FlowDiagramInformationCommand
                {
                    FlowDiagramInformationDto = new FlowDiagramDto
                    {
                        Name = flowDiagram.Name,
                    }
                };

                var result = await _mediator.Send(flowDiagramInfo);
                return Ok("Flow diagram created");
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an issue with the request");
                return BadRequest(ex.Message);
            }
        }
    }
}

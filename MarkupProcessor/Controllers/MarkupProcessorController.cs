using MarkupProcessor.Commands;
using MarkupProcessor.Handlers;
using MarkupProcessor.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace MarkupProcessor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarkupProcessorController : ControllerBase
    {
        private readonly ILogger<MarkupProcessorController> _logger;
        private readonly IMediator _mediator;

        public MarkupProcessorController(ILogger<MarkupProcessorController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet()]
        public async Task<IActionResult> Get(string flowChartId)
        {
            var result = await _mediator.Send(new MDContentsQuery(flowChartId));
            return Ok(result);
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> Post(string flowDiagramId)
        {
            try
            {
                var files = Request.Form.Files;
                
                var result = files.SelectMany(file =>
                {
                    var contents = new List<AddMDContentCommand>();
                    var reader = new StreamReader(file.OpenReadStream());
                    return ReadFileContents(contents, reader);
                });

                if(!result.Any())
                {
                    _logger.LogWarning("No JSON Content was found");
                    return Ok("Either no file was provided or no JSON content was found");
                }

                foreach(var item in result)
                {
                    item.MDContentsDto.FlowChartId = flowDiagramId;
                    var response = await _mediator.Send(item);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an issue with the request");
                return BadRequest(ex.Message);
            }
        }

        private List<AddMDContentCommand> ReadFileContents(List<AddMDContentCommand> contents, StreamReader reader)
        {
            while (reader.Peek() >= 0)
            {
                if (reader.ReadLine() == "```json")
                {
                    var jsonString = reader.ReadToEnd().Trim();
                    contents.Add(new AddMDContentCommand(JsonConvert.DeserializeObject<MDContentsDto>(jsonString.Remove(jsonString.Length - 3))));
                }
            }
            return contents;
        }
    }
}
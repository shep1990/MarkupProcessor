using MarkupProcessor.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        [HttpGet]
        public string Get()
        {
            return "hello world";
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> Post()
        {
            try
            {
                var file = Request.Form.Files[0];
                var contents = new AddMDContentCommand();
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                    {
                        if (reader.ReadLine() == "```json")
                        {
                            var jsonString = reader.ReadToEnd().Trim();
                            contents.MDContentsDto = JsonConvert.DeserializeObject<MDContentsDto>(jsonString.Remove(jsonString.Length - 3));
                            contents.MDContentsDto.FlowChartId = Guid.NewGuid().ToString();
                        }
                    }
                }

                if(contents.MDContentsDto != null)
                {
                    var result = await _mediator.Send(contents);
                    return Ok("file uploaded");
                }

                _logger.LogWarning("No JSON Content was found");
                return Ok("No file uploaded");
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an issue with the request");
                return BadRequest(ex.Message);
            }
        }
    }
}
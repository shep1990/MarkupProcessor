using MarkupProcessor.Application.Commands;
using MarkupProcessor.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MarkupProcessor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MarkupProcessorController : ControllerBase
    {
        private readonly ILogger<MarkupProcessorController> _logger;
        private readonly IMediator _mediator;

        public MarkupProcessorController(ILogger<MarkupProcessorController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file)
        {
            try
            {
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

                var result = await _mediator.Send(contents);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an issue with the request");
                return BadRequest(ex.Message);
            }
        }
    }
}
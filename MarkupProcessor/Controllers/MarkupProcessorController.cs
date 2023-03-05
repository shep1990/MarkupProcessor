using MarkupProcessor.Data;
using MarkupProcessor.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MarkupProcessor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MarkupProcessorController : ControllerBase
    {
        private readonly ILogger<MarkupProcessorController> _logger;
        private readonly IMarkupRepository _markupRepository;

        public MarkupProcessorController(ILogger<MarkupProcessorController> logger, IMarkupRepository markupRepository)
        {
            _logger = logger;
            _markupRepository = markupRepository;
        }

        [HttpPost]
        public async Task<MDContents> Post(IFormFile file)
        {
            var contents = new MDContents();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    if (reader.ReadLine() == "```json")
                    {
                        var jsonString = reader.ReadToEnd().Trim();
                        contents = JsonConvert.DeserializeObject<MDContents>(jsonString.Remove(jsonString.Length-3));
                        contents.FlowChartId = Guid.NewGuid().ToString();
                    }
                }
            }

            await _markupRepository.Add(contents);

           
            return contents;
        }
    }
}
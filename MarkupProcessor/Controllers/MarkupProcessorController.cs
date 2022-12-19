using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace MarkupProcessor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MarkupProcessorController : ControllerBase
    {
        private readonly ILogger<MarkupProcessorController> _logger;

        public MarkupProcessorController(ILogger<MarkupProcessorController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<MDContents> Get(IFormFile file)
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
                    }
                }
            }

            return contents;
        }
    }

    public class MDContents
    {
        public string Guid { get; set; }
        public string CreationDate { get; set; }
        public string Version { get; set; }
        public string SourceSystem { get; set; }
        public dynamic Payload { get; set; }
    }
}
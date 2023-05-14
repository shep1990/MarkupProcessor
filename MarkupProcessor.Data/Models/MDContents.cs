using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupProcessor.Data.Models
{
    public class MDContents
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        public string CreationDate { get; set; } = null!;
        public string Version { get; set; } = null!;
        public string SourceSystem { get; set; } = null!; 
        public string FlowChartId { get; set; } = null!;
        public dynamic Payload { get; set; } = null!;
    }
}

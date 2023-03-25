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
        public string Guid { get; set; }
        public string CreationDate { get; set; }
        public string Version { get; set; }
        public string SourceSystem { get; set; }
        public string FlowChartId { get; set; }
        public dynamic Payload { get; set; }
    }
}

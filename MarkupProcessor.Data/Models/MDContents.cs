using Microsoft.Azure.Documents;
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
        public MDContents(
            string flowChartId,
            DateTime creationDate,
            string version,
            string sourceSystem,
            string eventName,
            dynamic payload
        )
        {
            Id = Guid.NewGuid();
            FlowChartId = flowChartId;
            CreationDate = creationDate;
            Version = version;
            SourceSystem = sourceSystem;
            EventName = eventName;
            Payload = payload;
        }

        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty(PropertyName = "flowChartId")]
        public string FlowChartId { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public string Version { get; set; } = null!;
        public string SourceSystem { get; set; } = null!; 
        public string EventName { get; set; } = null!;
        public dynamic Payload { get; set; } = null!;
    }
}

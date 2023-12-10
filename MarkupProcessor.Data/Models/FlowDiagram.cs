using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupProcessor.Data.Models
{
    public class FlowDiagram
    {
        public FlowDiagram()
        {
                
        }
        public FlowDiagram(string flowDiagramName)
        {
            Id = Guid.NewGuid();
            FlowDiagramName = flowDiagramName;
        }
        [JsonProperty("id")]
        public Guid Id { get; set; }
        public string FlowDiagramName { get; set; } = null!;
    }
}

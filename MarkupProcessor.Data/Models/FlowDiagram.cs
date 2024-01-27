using Newtonsoft.Json;

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

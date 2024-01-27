using MarkupProcessor.Data.Models;

namespace MarkupProcessor.Data.Interfaces
{
    public interface IFlowDiagramInformationRepository
    {
        Task<FlowDiagram> Add(FlowDiagram diagramInformation);
        Task<List<FlowDiagram>> Get();
    }
}

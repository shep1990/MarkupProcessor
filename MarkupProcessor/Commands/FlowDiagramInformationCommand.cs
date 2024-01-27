using MarkupProcessor.Data.Models;
using MarkupProcessor.Handlers;
using MediatR;

namespace MarkupProcessor.Commands
{
    public class FlowDiagramInformationCommand : IRequest<HandlerResponse<FlowDiagram>>
    {
        public FlowDiagramInformationCommand(FlowDiagram flowDiagramInformationDto)
        {
            FlowDiagramInformation = flowDiagramInformationDto;
        }
        public FlowDiagram FlowDiagramInformation { get; set; } = null!;
    }
}

using MarkupProcessor.Application.Dto;
using MarkupProcessor.Handlers;
using MediatR;

namespace MarkupProcessor.Queries
{
    public class FlowDiagramQuery : IRequest<HandlerResponse<List<FlowDiagramDto>>>
    {
    }
}

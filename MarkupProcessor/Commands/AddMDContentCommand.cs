using MarkupProcessor.Handlers;
using MediatR;

namespace MarkupProcessor.Commands
{
    public class AddMDContentCommand : IRequest<HandlerResponse>
    {
        public MDContentsDto MDContentsDto { get; set; } = null!;
    }

    public class MDContentsDto
    {
        public Guid Id { get; set; }
        public string CreationDate { get; set; } = null!;
        public string Version { get; set; } = null!;
        public string SourceSystem { get; set; } = null!;
        public string FlowChartId { get; set; } = null!;
        public dynamic Payload { get; set; } = null!;
    }
}

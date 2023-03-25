using MediatR;

namespace MarkupProcessor.Application.Commands
{
    public class AddMDContentCommand : IRequest<HandlerResponse>
    {
        public MDContentsDto MDContentsDto { get; set; }
    }

    public class MDContentsDto
    {
        public string Guid { get; set; }
        public string CreationDate { get; set; }
        public string Version { get; set; }
        public string SourceSystem { get; set; }
        public string FlowChartId { get; set; }
        public dynamic Payload { get; set; }
    }
}

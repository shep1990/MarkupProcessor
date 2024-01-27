using MarkupProcessor.Data.Models;
using MarkupProcessor.Handlers;
using MediatR;

namespace MarkupProcessor.Commands
{
    public class AddMDContentCommand : IRequest<HandlerResponse<MDContents>>
    {
        public AddMDContentCommand(MDContentsDto MDContents)
        {
            MDContentsDto = MDContents;
        }

        public MDContentsDto MDContentsDto { get; set; } = null!;
    }

    public class MDContentsDto
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Version { get; set; } = null!;
        public string SourceSystem { get; set; } = null!;
        public string FlowChartId { get; set; } = null!;
        public string EventName { get; set; } = null!;
        public dynamic Payload { get; set; } = null!;
    }
}

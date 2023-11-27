using MarkupProcessor.Application.Dto;
using MarkupProcessor.Handlers;
using MediatR;

namespace MarkupProcessor.Queries
{
    public class MDContentsQuery : IRequest<HandlerResponse<List<MDContentsDto>>>
    {
        public MDContentsQuery(string id) { 
            Id = id;
        }
        public string Id { get; set; }
    }
}

using MarkupProcessor.Application.Dto;
using MarkupProcessor.Data.Interfaces;
using MarkupProcessor.Queries;
using MediatR;
using Newtonsoft.Json;

namespace MarkupProcessor.Handlers
{
    public class MDContentsQueryHandler : HandlerBase, IRequestHandler<MDContentsQuery, HandlerResponse<List<MDContentsDto>>>
    {
        public IMarkupRepository _markupRepository;

        public MDContentsQueryHandler(
            IMarkupRepository markupRepository,
            ILogger<MDContentsQueryHandler> logger) : base(logger)
        {
            _markupRepository = markupRepository;
        }

        public async Task<HandlerResponse<List<MDContentsDto>>> Handle(MDContentsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _markupRepository.Get(request.Id);
                var mdcontentsList = new List<MDContentsDto>();

                mdcontentsList = response.Select(x => new MDContentsDto(JsonConvert.SerializeObject(x.Payload, Formatting.Indented), x.EventName)).ToList();

                return new HandlerResponse<List<MDContentsDto>>
                {
                    Success = true,
                    Data = mdcontentsList
                };
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "There was an issue with the request");
                return new HandlerResponse<List<MDContentsDto>>
                {
                    Success = false,
                    Message = "There was an issue with the request"
                };
            }
        }
    }
}

using MarkupProcessor.Commands;
using MarkupProcessor.Controllers;
using MarkupProcessor.Data.Interfaces;
using MarkupProcessor.Data.Models;
using MediatR;

namespace MarkupProcessor.Handlers
{
    public class AddMDContentCommandHandler : HandlerBase, IRequestHandler<AddMDContentCommand, HandlerResponse>
    {
        public IMarkupRepository _markupRepository;

        public AddMDContentCommandHandler(IMarkupRepository markupRepository, ILogger<AddMDContentCommandHandler> logger) : base(logger) 
        {
            _markupRepository = markupRepository;
        }

        public async Task<HandlerResponse> Handle(AddMDContentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _markupRepository.Add(new MDContents
                {
                    Payload = request.MDContentsDto.Payload,
                    CreationDate = request.MDContentsDto.CreationDate,
                    SourceSystem = request.MDContentsDto.SourceSystem,
                    FlowChartId = request.MDContentsDto.FlowChartId,
                    Version = request.MDContentsDto.Version
                });

                return new HandlerResponse<MDContents> { Success = true, Data = result };
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "There was an issue with the request");
                return new HandlerResponse { Success = false, Message = "There was an issue with the request" };
            }
        }
    }
}

using MarkupProcessor.Commands;
using MarkupProcessor.Data.Interfaces;
using MarkupProcessor.Data.Models;
using MediatR;

namespace MarkupProcessor.Handlers
{
    public class AddMDContentCommandHandler : IRequestHandler<AddMDContentCommand, HandlerResponse>
    {
        public IMarkupRepository _markupRepository;
        public Logger<AddMDContentCommandHandler> _logger;

        public AddMDContentCommandHandler(IMarkupRepository markupRepository, Logger<AddMDContentCommandHandler> logger)
        {
            _markupRepository = markupRepository;
            _logger = logger;
        }

        public async Task<HandlerResponse> Handle(AddMDContentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _markupRepository.Add(new MDContents
                {
                    Payload = request.MDContentsDto.Payload,
                    CreationDate = request.MDContentsDto.CreationDate,
                    SourceSystem = request.MDContentsDto.SourceSystem,
                    FlowChartId = request.MDContentsDto.FlowChartId,
                    Id = request.MDContentsDto.Id,
                    Version = request.MDContentsDto.Version
                });

                return new HandlerResponse { Success = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an issue with the request");
                return new HandlerResponse { Success = false, Message = "There was an issue with the request" };
            }
        }
    }
}

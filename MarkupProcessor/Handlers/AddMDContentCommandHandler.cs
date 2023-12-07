using MarkupProcessor.Commands;
using MarkupProcessor.Controllers;
using MarkupProcessor.Data.Interfaces;
using MarkupProcessor.Data.Models;
using MediatR;

namespace MarkupProcessor.Handlers
{
    public class AddMDContentCommandHandler : HandlerBase, IRequestHandler<AddMDContentCommand, HandlerResponse<MDContents>>
    {
        public IMarkupRepository _markupRepository;

        public AddMDContentCommandHandler(IMarkupRepository markupRepository, ILogger<AddMDContentCommandHandler> logger) : base(logger) 
        {
            _markupRepository = markupRepository;
        }

        public async Task<HandlerResponse<MDContents>> Handle(AddMDContentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return new HandlerResponse<MDContents>
                {
                    Success = true,
                    Data = await _markupRepository.Add(new MDContents
                    (
                        request.MDContentsDto.Payload,
                        request.MDContentsDto.CreationDate,
                        request.MDContentsDto.SourceSystem,
                        request.MDContentsDto.FlowChartId,
                        request.MDContentsDto.Version
                    ))
                };
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "There was an issue with the request");
                return new HandlerResponse<MDContents> { Success = false, Message = "There was an issue with the request" };
            }
        }
    }
}

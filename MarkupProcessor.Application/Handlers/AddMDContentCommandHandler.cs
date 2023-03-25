using Azure;
using MarkupProcessor.Application.Commands;
using MarkupProcessor.Data.Interfaces;
using MarkupProcessor.Data.Models;
using MediatR;
using Microsoft.Azure.Cosmos;

namespace MarkupProcessor.Application.Handlers
{
    public class AddMDContentCommandHandler : IRequestHandler<AddMDContentCommand, HandlerResponse>
    {
        public IMarkupRepository _markupRepository;

        public AddMDContentCommandHandler(IMarkupRepository markupRepository)
        {
            _markupRepository = markupRepository;
        }

        public async Task<HandlerResponse> Handle(AddMDContentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mdContentsEntity = new MDContents
                {
                    Payload = request.MDContentsDto.Payload,
                    CreationDate = request.MDContentsDto.CreationDate,
                    SourceSystem = request.MDContentsDto.SourceSystem,
                    FlowChartId = request.MDContentsDto.FlowChartId,
                    Guid = request.MDContentsDto.Guid,
                    Version = request.MDContentsDto.Version
                };

                await _markupRepository.Add(mdContentsEntity);

                return new HandlerResponse { Success = true };
            }
            catch (Exception ex)
            {
                return new HandlerResponse { Success = false, Message = "There was an issue with the request" };
            }
        }
    }
}

using MarkupProcessor.Application.Dto;
using MarkupProcessor.Commands;
using MarkupProcessor.Data.Interfaces;
using MarkupProcessor.Data.Models;
using MediatR;

namespace MarkupProcessor.Handlers
{
    public class FlowDiagramInformationCommandHandler : HandlerBase, IRequestHandler<FlowDiagramInformationCommand, HandlerResponse<FlowDiagramDto>>
    {
        public IFlowDiagramInformationRepository _flowDiagramInformationRepository;

        public FlowDiagramInformationCommandHandler(
            IFlowDiagramInformationRepository flowDiagramInformationRepository,
            ILogger<FlowDiagramInformationCommandHandler> logger) : base(logger)
        {
            _flowDiagramInformationRepository = flowDiagramInformationRepository;
        }

        public async Task<HandlerResponse<FlowDiagramDto>> Handle(FlowDiagramInformationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _flowDiagramInformationRepository.Add(new FlowDiagram
                {
                    FlowDiagramName = request.FlowDiagramInformationDto.Name
                });

                return new HandlerResponse<FlowDiagramDto> { Success = true, Data =  new FlowDiagramDto { Id = response.Id, Name = response.FlowDiagramName } };
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "There was an issue with the request");
                return new HandlerResponse<FlowDiagramDto> { Success = false, Message = "There was an issue with the request" };
            }
        }
    }
}

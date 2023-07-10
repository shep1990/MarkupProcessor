using MarkupProcessor.Application.Dto;
using MarkupProcessor.Commands;
using MarkupProcessor.Data.Interfaces;
using MarkupProcessor.Data.Models;
using MediatR;

namespace MarkupProcessor.Handlers
{
    public class FlowDiagramInformationCommandHandler : IRequestHandler<FlowDiagramInformationCommand, HandlerResponse<FlowDiagramDto>>
    {
        public IFlowDiagramInformationRepository _flowDiagramInformationRepository;
        private readonly ILogger<FlowDiagramInformationCommandHandler> _logger;

        public FlowDiagramInformationCommandHandler(
            IFlowDiagramInformationRepository flowDiagramInformationRepository,
            ILogger<FlowDiagramInformationCommandHandler> logger)
        {
            _flowDiagramInformationRepository = flowDiagramInformationRepository;
            _logger = logger;
        }

        public async Task<HandlerResponse<FlowDiagramDto>> Handle(FlowDiagramInformationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _flowDiagramInformationRepository.Add(new FlowDiagram
                {
                    FlowDiagramName = request.FlowDiagramInformationDto.Name
                });

                return new HandlerResponse<FlowDiagramDto> { Success = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an issue with the request");
                return new HandlerResponse<FlowDiagramDto> { Success = false, Message = "There was an issue with the request" };
            }
        }
    }
}

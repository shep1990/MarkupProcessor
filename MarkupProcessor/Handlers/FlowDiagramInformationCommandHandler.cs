using MarkupProcessor.Commands;
using MarkupProcessor.Data.Interfaces;
using MarkupProcessor.Data.Models;
using MediatR;

namespace MarkupProcessor.Handlers
{
    public class FlowDiagramInformationCommandHandler : IRequestHandler<FlowDiagramInformationCommand, HandlerResponse>
    {
        public IFlowDiagramInformationRepository _flowDiagramInformationRepository;
        public ILogger<FlowDiagramInformationCommandHandler> _logger;

        public FlowDiagramInformationCommandHandler(
            IFlowDiagramInformationRepository flowDiagramInformationRepository,
            ILogger<FlowDiagramInformationCommandHandler> logger)
        {
            _flowDiagramInformationRepository = flowDiagramInformationRepository;
            _logger = logger;
        }

        public async Task<HandlerResponse> Handle(FlowDiagramInformationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _flowDiagramInformationRepository.Add(new FlowDiagram
                {
                    FlowDiagramName = request.FlowDiagramInformationDto.Name
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

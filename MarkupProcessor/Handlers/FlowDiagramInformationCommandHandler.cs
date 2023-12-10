using MarkupProcessor.Application.Dto;
using MarkupProcessor.Commands;
using MarkupProcessor.Data.Interfaces;
using MarkupProcessor.Data.Models;
using MediatR;

namespace MarkupProcessor.Handlers
{
    public class FlowDiagramInformationCommandHandler : HandlerBase, IRequestHandler<FlowDiagramInformationCommand, HandlerResponse<FlowDiagram>>
    {
        public IFlowDiagramInformationRepository _flowDiagramInformationRepository;

        public FlowDiagramInformationCommandHandler(
            IFlowDiagramInformationRepository flowDiagramInformationRepository,
            ILogger<FlowDiagramInformationCommandHandler> logger) : base(logger)
        {
            _flowDiagramInformationRepository = flowDiagramInformationRepository;
        }

        public async Task<HandlerResponse<FlowDiagram>> Handle(FlowDiagramInformationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _flowDiagramInformationRepository.Add(new FlowDiagram(request.FlowDiagramInformation.FlowDiagramName));

                return new HandlerResponse<FlowDiagram> { Success = true, Data = response };
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "There was an issue with the request");
                return new HandlerResponse<FlowDiagram> { Success = false, Message = "There was an issue with the request" };
            }
        }
    }
}

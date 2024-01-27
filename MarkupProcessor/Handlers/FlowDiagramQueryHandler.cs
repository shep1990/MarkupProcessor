using MarkupProcessor.Application.Dto;
using MarkupProcessor.Data.Interfaces;
using MarkupProcessor.Queries;
using MediatR;

namespace MarkupProcessor.Handlers
{
    public class FlowDiagramQueryHandler : HandlerBase, IRequestHandler<FlowDiagramQuery, HandlerResponse<List<FlowDiagramDto>>>
    {
        public IFlowDiagramInformationRepository _flowDiagramInformationRepository;

        public FlowDiagramQueryHandler(
            IFlowDiagramInformationRepository flowDiagramInformationRepository,
            ILogger<FlowDiagramQueryHandler> logger) : base(logger)
        {
            _flowDiagramInformationRepository = flowDiagramInformationRepository;
        }

        public async Task<HandlerResponse<List<FlowDiagramDto>>> Handle(FlowDiagramQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _flowDiagramInformationRepository.Get();
                var flowDiagramList = new List<FlowDiagramDto>();

                flowDiagramList = response.Select(x => new FlowDiagramDto(x.Id, x.FlowDiagramName)).ToList();

                return new HandlerResponse<List<FlowDiagramDto>>
                {
                    Success = true,
                    Data = flowDiagramList
                };
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "There was an issue with the request");
                return new HandlerResponse<List<FlowDiagramDto>>
                {
                    Success = false,
                    Message = "There was an issue with the request"
                };
            }

        }
    }
}

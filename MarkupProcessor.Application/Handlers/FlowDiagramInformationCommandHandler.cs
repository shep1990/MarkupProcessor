using MarkupProcessor.Application.Commands;
using MarkupProcessor.Data.Interfaces;
using MarkupProcessor.Data.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupProcessor.Application.Handlers
{
    public class FlowDiagramInformationCommandHandler : IRequestHandler<FlowDiagramInformationCommand, HandlerResponse>
    {
        public IFlowDiagramInformationRepository _flowDiagramInformationRepository;

        public FlowDiagramInformationCommandHandler(IFlowDiagramInformationRepository flowDiagramInformationRepository)
        {
            _flowDiagramInformationRepository = flowDiagramInformationRepository;
        }

        public async Task<HandlerResponse> Handle(FlowDiagramInformationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var flowDiagramInformation = new FlowDiagram
                {
                    FlowDiagramName = request.FlowDiagramInformationDto.Name
                };

                return new HandlerResponse { Success = true };
            }
            catch (Exception ex)
            {
                return new HandlerResponse { Success = false, Message = "There was an issue with the request" };
            }
        }
    }
}

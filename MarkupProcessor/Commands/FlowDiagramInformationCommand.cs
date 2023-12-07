using MarkupProcessor.Application.Dto;
using MarkupProcessor.Handlers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupProcessor.Commands
{
    public class FlowDiagramInformationCommand : IRequest<HandlerResponse<FlowDiagramDto>>
    {
        public FlowDiagramInformationCommand(FlowDiagramDto flowDiagramInformationDto)
        {
            FlowDiagramInformationDto = flowDiagramInformationDto;
        }
        public FlowDiagramDto FlowDiagramInformationDto { get; set; } = null!;
    }
}

using MarkupProcessor.Application.Dto;
using MarkupProcessor.Data.Models;
using MarkupProcessor.Handlers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupProcessor.Commands
{
    public class FlowDiagramInformationCommand : IRequest<HandlerResponse<FlowDiagram>>
    {
        public FlowDiagramInformationCommand(FlowDiagram flowDiagramInformationDto)
        {
            FlowDiagramInformation = flowDiagramInformationDto;
        }
        public FlowDiagram FlowDiagramInformation { get; set; } = null!;
    }
}

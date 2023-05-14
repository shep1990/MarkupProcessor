using MarkupProcessor.Application.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupProcessor.Application.Commands
{
    public class FlowDiagramInformationCommand : IRequest<HandlerResponse>
    {
        public FlowDiagramDto FlowDiagramInformationDto { get; set; } = null!;
    }
}

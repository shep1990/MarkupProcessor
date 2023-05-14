using MarkupProcessor.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupProcessor.Data.Interfaces
{
    public interface IFlowDiagramInformationRepository
    {
        Task<FlowDiagram> Add(FlowDiagram diagramInformation);
    }
}

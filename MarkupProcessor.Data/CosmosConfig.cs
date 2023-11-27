using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupProcessor.Data
{
    public class CosmosConfig
    {
        public string Database { get; set; }
        public string FlowDiagramContainer { get; set; }
        public string FlowDiagramPartition { get; set; }
        public string MarkupContainer { get; set; }
        public string MarkupPartition { get; set; }
    }
}

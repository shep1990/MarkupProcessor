using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupProcessor.Application.Dto
{
    public class MDContentsDto
    {
        public MDContentsDto(string payload)
        {
            Payload = payload;
        }

        public string Payload { get; set; }
    }
}

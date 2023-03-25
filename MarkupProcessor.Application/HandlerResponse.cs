using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupProcessor.Application
{
    public class HandlerResponse
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public string Message { get; set; }
    }

    public class HandlerRespose<TData> : HandlerResponse
    {
        public TData Data { get; set;}
    }

    public class HandlerResponseList<TData> : HandlerResponse
    {
        public List<TData> Data { get; set;}
    }
}

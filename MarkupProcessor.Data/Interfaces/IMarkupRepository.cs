using MarkupProcessor.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupProcessor.Data.Interfaces
{
    public interface IMarkupRepository
    {
        Task<MDContents> Add(MDContents contents);
        Task<List<MDContents>> Get(string flowchartId);
    }
}

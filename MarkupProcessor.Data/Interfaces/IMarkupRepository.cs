using MarkupProcessor.Data.Models;

namespace MarkupProcessor.Data.Interfaces
{
    public interface IMarkupRepository
    {
        Task<MDContents> Add(MDContents contents);
        Task<List<MDContents>> Get(string flowchartId);
    }
}

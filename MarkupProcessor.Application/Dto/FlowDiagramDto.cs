namespace MarkupProcessor.Application.Dto
{
    public class FlowDiagramDto
    {
        public FlowDiagramDto(Guid id, string name)
        {
            Id = id;
            FlowDiagramName = name;
        }

        public Guid Id { get; set; }
        public string FlowDiagramName { get; set; } = null!;
    }
}

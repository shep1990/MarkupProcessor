using FluentValidation;
using MarkupProcessor.Data.Models;

namespace MarkupProcessor.Validators
{
    public class FlowDiagramValidator : AbstractValidator<FlowDiagram>
    {
        public FlowDiagramValidator()
        {
            RuleFor(x => x.FlowDiagramName).NotEmpty();
        }
    }
}

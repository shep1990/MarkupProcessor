using FluentValidation;
using MarkupProcessor.Application.Dto;

namespace MarkupProcessor.Validators
{
    public class FlowDiagramValidator : AbstractValidator<FlowDiagramDto>
    {
        public FlowDiagramValidator() { 
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}

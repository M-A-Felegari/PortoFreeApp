using FluentValidation;

namespace PortoFree.Application.Features.WorkExamples.Commands.UpdateWorkExample;

public class UpdateWorkExampleCommandValidator : AbstractValidator<UpdateWorkExampleCommand>
{
    public UpdateWorkExampleCommandValidator()
    {
        RuleFor(a => a.Title)
            .Length(3, 50);
        
        RuleFor(a=>a.Description)
            .MaximumLength(300);
        
        RuleFor(a=>a.FinishDate)
            .GreaterThan(a=>a.StartDate);
    }
}
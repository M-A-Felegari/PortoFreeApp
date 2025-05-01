using FluentValidation;

namespace PortoFree.Application.Features.WorkExamples.Queries.GetWorkExamples;

public class GetWorkExamplesQueryValidator : AbstractValidator<GetWorkExamplesQuery>
{
    public GetWorkExamplesQueryValidator()
    {
        RuleFor(q => q.OwnerId)
            .NotNull()
            .GreaterThan(0);

        RuleFor(q => q.NextCursor)
            .NotNull()
            .GreaterThanOrEqualTo(0);

        RuleFor(q => q.Limit)
            .InclusiveBetween(1, 20); 
    }
}
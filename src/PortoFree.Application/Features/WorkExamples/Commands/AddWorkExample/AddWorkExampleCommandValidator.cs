using FluentValidation;
using PortoFree.Application.Services.FileValidation;

namespace PortoFree.Application.Features.WorkExamples.Commands.AddWorkExample;

public class AddWorkExampleCommandValidator : AbstractValidator<AddWorkExampleCommand>
{
    public AddWorkExampleCommandValidator()
    {
        IFileValidator fileValidator = new ImageValidator();
        RuleFor(a => a.Title)
            .Length(3, 50);
        
        RuleFor(a=>a.Description)
            .MaximumLength(300);
        
        RuleFor(a=>a.FinishDate)
            .GreaterThan(a=>a.StartDate);

        RuleFor(a => a.ImageFileStream)
            .Must(stream => fileValidator.IsSizeValid(stream!.Length, 1))
            .When(a => a.ImageFileStream != null);

        RuleFor(a => a)
            .Must(command =>
            {
                if (command.ImageFileStream == null || string.IsNullOrWhiteSpace(command.ImageFileName))
                    return true;

                if (command.ImageFileStream.CanSeek)
                    command.ImageFileStream.Seek(0, SeekOrigin.Begin);

                return fileValidator.IsFileValid(command.ImageFileStream, command.ImageFileName);
            })
            .WithMessage($"image format is not valid") //todo: add allowed formats
            .When(command=>
                command.ImageFileStream != null &&
                !string.IsNullOrWhiteSpace(command.ImageFileName));
    }
}
using MediatR;
using FluentValidation;
using Microsoft.AspNetCore.Http;

public class TextFileToTextContainerCommand : IRequest<TextContainerFile>
{
    public IFormFile File {get; set;}
}

public class TextFileToTextContainerValidator : AbstractValidator<TextFileToTextContainerCommand>
{
    public TextFileToTextContainerValidator()
    {
        
    }
}

class TextFileToTextContainerHandler : IRequestHandler<TextFileToTextContainerCommand, TextContainerFile>
{
    async Task<TextContainerFile> IRequestHandler<TextFileToTextContainerCommand, TextContainerFile>.Handle(TextFileToTextContainerCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine(request.File.FileName);
        var reader = new StreamReader(request.File.OpenReadStream());
        string textForFile = reader.ReadToEnd();

        Console.WriteLine(textForFile);
        return new TextContainerFile(textForFile);
    }
}
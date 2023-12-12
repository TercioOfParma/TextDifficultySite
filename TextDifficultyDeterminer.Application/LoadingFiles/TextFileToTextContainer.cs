using MediatR;
using FluentValidation;
using Microsoft.AspNetCore.Http;

public class TextFileToTextContainerCommand : IRequest<TextContainerFile>
{
    public string Text{get;set;}
    public string Filename {get; set;}
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
        return new TextContainerFile(request.Filename, request.Text);
    }
}

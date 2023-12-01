using MediatR;
using FluentValidation;
using Microsoft.AspNetCore.Http;

public class LoadFileIntoDatabaseCommand : IRequest<bool>
{
    public IFormFile File {get; set;}
    public Guid LanguageId {get; set;}
}

public class LoadFileIntoDatabaseValidator : AbstractValidator<LoadFileIntoDatabaseCommand>
{
    public LoadFileIntoDatabaseValidator()
    {
        
    }
}

public class LoadFileIntoDatabaseHandler : IRequestHandler<LoadFileIntoDatabaseCommand, bool> 
{
    protected IUnitOfWork _db {get; set;}
    public LoadFileIntoDatabaseHandler(IUnitOfWork db)
    {
        _db = db;
    }
    async Task<bool> IRequestHandler<LoadFileIntoDatabaseCommand, bool>.Handle(LoadFileIntoDatabaseCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine("Request Handler Hit!");
        var reader = new StreamReader(request.File.OpenReadStream());
        var textForFile = reader.ReadToEnd();

        var dictionaryGenerated = new TextContainerFile(request.File.FileName, textForFile);
        foreach(var word in dictionaryGenerated.FrequencyDictionaryForThisFile.Words)
        {
            word.Language = request.LanguageId;
        }

        await LoadIntoDatabaseService.LoadTextContainerFileIntoDatabase(dictionaryGenerated, request.LanguageId, _db);
        return true;
    }
}
using MediatR;
using FluentValidation;
using Microsoft.AspNetCore.Http;

public class LoadFileIntoDatabaseCommand : IRequest<bool>
{
    public Dictionary<string, string> FilesAndFilenames {get; set;}
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
        var containerList = new List<TextContainerFile>();
        foreach(var file in request.FilesAndFilenames)
        {
            Console.WriteLine(file.Key);
            var dictionaryGenerated = new TextContainerFile(file.Key, file.Value, request.LanguageId);
            containerList.Add(dictionaryGenerated);
        }

        await LoadIntoDatabaseService.LoadTextContainerFileIntoDatabase(containerList, request.LanguageId, _db);
        return true;
    }
}
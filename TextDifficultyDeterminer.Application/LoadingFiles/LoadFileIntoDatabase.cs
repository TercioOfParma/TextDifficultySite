

public class LoadFileIntoDatabaseCommand
{
    public IFormFile File {get; set;}
    public Guid LanguageId {get; set;}
}

public class LoadFileIntoDatabaseValidator : AbstractValidator<LoadFileIntoDatabaseCommand>
{
    public LoadFileIntoDatabase()
    {
        
    }
}

public class LoadFileIntoDatabaseHandler : IRequestHandler<LoadFileIntoDatabaseCommand, bool> 
{
    async Task<bool> IRequestHandler<LoadFileIntoDatabaseCommand, bool>.Handle(LoadFileIntoDatabaseCommand request, CancellationToken cancellationToken)
    {
        var reader = new StreamReader(request.File.OpenReadStream());
        var textForFile = reader.ReadToEnd();

        
        return true;
    }
}
using MediatR;

public class AddLanguageCommand : IRequest<bool>
{
    public Language Language {get; set;}
}
class AddLanguageHandler : IRequestHandler<AddLanguageCommand, bool>
{
    public IUnitOfWork unitOfWork{get; set;}
    public AddLanguageHandler(IUnitOfWork unit)
    {
        unitOfWork = unit;
    }

    async Task<bool> IRequestHandler<AddLanguageCommand, bool>.Handle(AddLanguageCommand request, CancellationToken cancellationToken)
    {
        if(unitOfWork.Languages.Where(x => x.LanguageName == request.Language.LanguageName).Count() == 0)
        {
            await unitOfWork.Languages.AddAsync(request.Language);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
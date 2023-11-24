using MediatR;

public class GetLanguagesQuery : IRequest<GetLanguagesResult>
{
}

public class GetLanguagesResult {

    public List<Language> LanguageList {get; set;}
}
class GetLanguagesHandler : IRequestHandler<GetLanguagesQuery, GetLanguagesResult>
{

    public IUnitOfWork unitOfWork{get; set;}
    public GetLanguagesHandler(IUnitOfWork unit)
    {
        unitOfWork = unit;
    }

    async Task<GetLanguagesResult> IRequestHandler<GetLanguagesQuery, GetLanguagesResult>.Handle(GetLanguagesQuery request, CancellationToken cancellationToken)
    {
        return new GetLanguagesResult { LanguageList = unitOfWork.Languages.ToList()}; 
    }
}
using MediatR;
using Microsoft.EntityFrameworkCore;
public class GetFrequencyDictionaryQuery : IRequest<GetFrequencyDictionaryResult>
{
    public Guid LanguageId {get; set;}
}

public class GetFrequencyDictionaryResult {

    public FrequencyDictionary Dictionary {get; set;}
}
class GetFrequencyDictionaryHandler : IRequestHandler<GetFrequencyDictionaryQuery, GetFrequencyDictionaryResult>
{

    public IUnitOfWork unitOfWork{get; set;}
    public GetFrequencyDictionaryHandler(IUnitOfWork unit)
    {
        unitOfWork = unit;
    }

    async Task<GetFrequencyDictionaryResult> IRequestHandler<GetFrequencyDictionaryQuery, GetFrequencyDictionaryResult>.Handle(GetFrequencyDictionaryQuery request, CancellationToken cancellationToken)
    {
        var words = await unitOfWork.Words.Where(x => x.Language == request.LanguageId).ToListAsync();
        var dictionary = new FrequencyDictionary(words);
        dictionary.OverallWordCount = words.Select(x => x.FrequencyOfWord).ToList().Aggregate((a,c) => a + c);
        Console.WriteLine(dictionary.OverallWordCount);
        return new GetFrequencyDictionaryResult { Dictionary = dictionary };
    }
}
using MediatR;
public class CheckTextAgainstDbQuery : IRequest<CheckTextAgainstDbResult>
{
    public Guid LanguageId {get; set;}
    public Dictionary<string, string> Files {get; set;}
}

public class CheckTextAgainstDbResult {

    public TextContainer Text {get; set;}
}
class CheckTextAgainstDbHandler : IRequestHandler<CheckTextAgainstDbQuery, CheckTextAgainstDbResult>
{

    public IMediator Mediator{get; set;}
    public CheckTextAgainstDbHandler(IMediator mediator)
    {
        Mediator = mediator;
    }

    async Task<CheckTextAgainstDbResult> IRequestHandler<CheckTextAgainstDbQuery, CheckTextAgainstDbResult>.Handle(CheckTextAgainstDbQuery request, CancellationToken cancellationToken)
    {
        var dictionary = (await Mediator.Send(new GetFrequencyDictionaryQuery { LanguageId = request.LanguageId})).Dictionary;
        var container = await GenerateTextContainer(request.Files);
        var tasks = new List<Task>();

        container.Files.ForEach(x => tasks.Add(Task.Run(() => x.GenerateScore(dictionary))));  
        
        await Task.WhenAll(tasks.ToArray());

        
        return new CheckTextAgainstDbResult { Text = container };
    }
    private async Task<TextContainer> GenerateTextContainer(Dictionary<string, string> files)
    {
        var tasks = new List<Task<TextContainerFile>>();
        foreach(var file in files)
        {
            tasks.Add(Task.Run(async () => await Mediator.Send(new TextFileToTextContainerCommand { Filename = file.Key, Text = file.Value})));
        }
        
        var containerList = await Task.WhenAll(tasks.ToArray());

        return new TextContainer(containerList.ToList(), false);
    }
}
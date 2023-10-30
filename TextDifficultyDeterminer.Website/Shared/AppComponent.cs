using Microsoft.AspNetCore.Components;
using MediatR;


namespace TextDifficultyDeterminer.Website.Shared 
{
    public class AppComponent : ComponentBase 
    {
        [Inject]
        protected IMediator Mediator {get; set;}
    }

}
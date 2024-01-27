using Microsoft.AspNetCore.Components;
using TextDifficultyDeterminer.Website.Shared;
using MediatR;


namespace TextDifficultyDeterminer.Website.Shared 
{
    public class AppComponent : ComponentBase 
    {
        [Inject]
        protected IMediator Mediator {get; set;}
        [Inject]
        protected NavigationManager NavigationManager{get; set;}

        protected bool Loading {get; set;}
        protected bool Error {get; set;}
        protected bool Complete {get; set;}
    }

}
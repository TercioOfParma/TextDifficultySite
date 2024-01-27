

using TextDifficultyDeterminer.Website.Shared;

namespace TextDifficultyDeterminer.Website.AddLanguage
{
    public class AddLanguageBase : AppComponent 
    {
        protected Language Language {get; set; } = new();
        protected async Task Submit()
        {
            var result = await Mediator.Send(new AddLanguageCommand { Language = Language });
            Complete = result;
            Error = !result;
        }
    }
}
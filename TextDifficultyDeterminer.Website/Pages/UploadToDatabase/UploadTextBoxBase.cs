using TextDifficultyDeterminer.Website.Shared;
using TextDifficultyDeterminer.Website.Dtos;
using Radzen.Blazor;
using Radzen;
namespace TextDifficultyDeterminer.Website.UploadToDatabase 
{
    public class UploadTextBoxBase : AppComponent 
    {
        public TestCorpusUploadDto TestCorpusUploadDto {get; set; } = new();
        public Guid LanguageId {get; set;}
        public List<Language> LanguageList {get; set;}
        public RadzenUpload Upload {get;set;} = new();
        public bool IsUploaded {get; set;} = false;

        protected async override Task OnInitializedAsync()
        {
            var languages = await Mediator.Send(new GetLanguagesQuery {} );
            LanguageList = languages.LanguageList;
        }

        protected async Task HandleResponseUploadToDatabase(UploadCompleteEventArgs e)
        {
            Console.WriteLine("DONE!");
            IsUploaded = true;
        }
    }
}
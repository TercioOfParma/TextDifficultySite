using TextDifficultyDeterminer.Website.Shared;
using TextDifficultyDeterminer.Website.Services;
using TextDifficultyDeterminer.Website.Dtos;
using Radzen.Blazor;
using Radzen;
using Microsoft.JSInterop;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using ClosedXML.Excel;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;

namespace TextDifficultyDeterminer.Website.UploadToDatabase 
{
    public class UploadTextBoxBase : AppComponent 
    {
        public TestCorpusUploadDto TestCorpusUploadDto {get; set; } = new();
        [Inject]
        protected ProcessTextFiles TextFiles {get; set;}
        public Guid LanguageId {get; set;}
        public List<Language> LanguageList {get; set;}
        public RadzenUpload Upload {get;set;} = new();
        public bool IsUploaded {get; set;} = false;
        public IReadOnlyList<IBrowserFile> FilesToUpload {get; set;}
        private const int MAX_TEXT_FILES = 1000;
        protected async override Task OnInitializedAsync()
        {
            var languages = await Mediator.Send(new GetLanguagesQuery {} );
            LanguageList = languages.LanguageList;
        }
        public async Task UpdateFiles(InputFileChangeEventArgs e) =>
            FilesToUpload = e.GetMultipleFiles(MAX_TEXT_FILES);
        protected async Task HandleCorpus()
        {
            var fileList = FilesToUpload;
            Dictionary<string, string> dict = new();
            foreach(var file in fileList)
            {
                if(file.ContentType != "text/plain")
                    continue;
                var reader = new StreamReader(file.OpenReadStream());
                var textForFile = await reader.ReadToEndAsync();
                dict[file.Name] = textForFile;
            }

            await TextFiles.LoadFilesIntoDatabase(LanguageId, dict);
        }

    }
}
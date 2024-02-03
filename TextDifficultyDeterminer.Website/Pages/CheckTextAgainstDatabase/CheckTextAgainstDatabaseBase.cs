using MediatR;
using TextDifficultyDeterminer.Website.Shared;
using TextDifficultyDeterminer.Website.Dtos;
using Microsoft.AspNetCore.Components.Forms;
using Radzen.Blazor;
using Radzen;
using Microsoft.JSInterop;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using ClosedXML.Excel;

namespace TextDifficultyDeterminer.Website.CheckAgainstDatabase 
{
    public class CheckTextAgainstDatabaseBase : AppComponent 
    {
        [Inject]
        public IJSRuntime JS {get; set;}
        public Guid LanguageId {get; set;}
        public List<Language> LanguageList {get; set;}
        public IReadOnlyList<IBrowserFile> FilesToUpload {get; set;}
        private const int MAX_TEXT_FILES = 10000;

        protected async override Task OnInitializedAsync()
        {
            var languages = await Mediator.Send(new GetLanguagesQuery {} );
            LanguageList = languages.LanguageList;
        }

        public async Task UpdateFiles(InputFileChangeEventArgs e) =>
            FilesToUpload = e.GetMultipleFiles(MAX_TEXT_FILES);

        protected async Task HandleCorpus()
        {
            Loading = true;
            var timeBefore = DateTime.Now;
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

            var container = await Mediator.Send(new CheckTextAgainstDbQuery { Files = dict, LanguageId = LanguageId});
            var excelFile = await Mediator.Send(new TextContainerToExcelCommand { Container = container.Text});
            var stream = new MemoryStream();
            excelFile.SaveAs(stream);
            stream.Position = 0;
            using var streamRef = new DotNetStreamReference(stream: stream);
            Complete = true;
            Loading = false;
            var timeAfter = DateTime.Now;
            Console.WriteLine($"Time {(timeAfter - timeBefore).TotalSeconds}");
            await JS.InvokeVoidAsync("downloadFileFromStream", "Test.xlsx", streamRef);
        }

    }
}
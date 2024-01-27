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

namespace TextDifficultyDeterminer.Website.TestCorpus 
{
    public class TestCorpusBase : AppComponent
    {
        [Inject]
        public IJSRuntime JS {get; set;}
        [Inject]
        protected ProcessTextFiles TextFiles {get; set;}
        public TestCorpusUploadDto TestCorpusUploadDto {get; set; } = new();
        public RadzenUpload Upload {get;set;} = new();
        public IReadOnlyList<IBrowserFile> FilesToUpload {get; set;}
        private const int MAX_TEXT_FILES = 1000;

        public async Task UpdateFiles(InputFileChangeEventArgs e) =>
            FilesToUpload = e.GetMultipleFiles(MAX_TEXT_FILES);
        public async Task HandleCorpus()
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

            var container = await TextFiles.LoadFiles(dict);
            var excelFile = await Mediator.Send(new TextContainerToExcelCommand { Container = container});
            
            var stream = new MemoryStream();
            excelFile.SaveAs(stream);
            stream.Position = 0;
            using var streamRef = new DotNetStreamReference(stream: stream);

            await JS.InvokeVoidAsync("downloadFileFromStream", "Test.xlsx", streamRef);
        }

    }

}
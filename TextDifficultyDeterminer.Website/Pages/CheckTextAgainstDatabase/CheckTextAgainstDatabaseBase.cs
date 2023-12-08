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
        public bool IsNotZip {get; set;} = false;
        public Guid LanguageId {get; set;}
        public List<Language> LanguageList {get; set;}
        public RadzenUpload Upload {get;set;} = new();
        public bool IsUploaded {get; set;} = false;

        protected async override Task OnInitializedAsync()
        {
            var languages = await Mediator.Send(new GetLanguagesQuery {} );
            LanguageList = languages.LanguageList;
        }

        protected async Task HandleDownloadExcel(UploadCompleteEventArgs e)
        {

        }

    }
}
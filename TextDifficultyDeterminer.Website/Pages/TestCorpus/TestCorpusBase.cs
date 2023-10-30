using TextDifficultyDeterminer.Website.Shared;
using TextDifficultyDeterminer.Website.Dtos;
using Microsoft.AspNetCore.Components.Forms;
using Radzen.Blazor;

namespace TextDifficultyDeterminer.Website.TestCorpus 
{
    public class TestCorpusBase : AppComponent
    {
        public TestCorpusUploadDto TestCorpusUploadDto {get; set; } = new();
        public bool IsNotZip {get; set;} = false;
        public RadzenUpload Upload {get;set;} = new();

    }

}
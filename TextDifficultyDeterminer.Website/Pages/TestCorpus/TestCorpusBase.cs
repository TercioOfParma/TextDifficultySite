using TextDifficultyDeterminer.Website.Shared;
using TextDifficultyDeterminer.Website.Dtos;
using Microsoft.AspNetCore.Components.Forms;

namespace TextDifficultyDeterminer.Website.TestCorpus 
{
    public class TestCorpusBase : AppComponent
    {
        public TestCorpusUploadDto TestCorpusUploadDto {get; set; } = new();
        public bool IsNotZip {get; set;} = false;

        public async Task LoadZipFile(InputFileChangeEventArgs e)
        {
            IsNotZip = false; 
            if(e.File.ContentType != "application/zip")
                IsNotZip = true;
            else 
                TestCorpusUploadDto.ZipFile = e.File;
        }

    }

}
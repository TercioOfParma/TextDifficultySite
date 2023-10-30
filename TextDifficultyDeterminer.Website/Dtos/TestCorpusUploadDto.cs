using Microsoft.AspNetCore.Components.Forms; 


namespace TextDifficultyDeterminer.Website.Dtos 
{
    public class TestCorpusUploadDto 
    {
        public IBrowserFile ZipFile {get; set;}
    }
}
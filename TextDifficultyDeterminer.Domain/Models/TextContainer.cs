

public class TextContainer 
{
    
    public List<TextContainerFile> Files {get; set;}
    public FrequencyDictionary ConcatDictionary {get; set;}
    public TextContainer(List<TextContainerFile> files)
    {
        Files = files;
    }
    public async Task GenerateConcatDictionary()
    {
        this.ConcatDictionary  = new FrequencyDictionary();
        foreach(var file in Files)
        {
            var dict = file.FrequencyDictionaryForThisFile;
            ConcatDictionary.ApplyFrequencyWords(dict.Words);
        }
        ConcatDictionary.CalculateDifficultyScores();
    }
}
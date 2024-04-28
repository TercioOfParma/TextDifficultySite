

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
        var i = 1;
        foreach(var file in Files)
        {
            var dict = file.FrequencyDictionaryForThisFile;
            Console.WriteLine($"{i}");
            i++;
            ConcatDictionary.ApplyFrequencyWords(dict.Words);
        }
        ConcatDictionary.CalculateDifficultyScores();
    }
}
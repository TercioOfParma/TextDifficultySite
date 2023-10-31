

public class TextContainer 
{
    
    public List<TextContainerFile> Files {get; set;}
    public FrequencyDictionary ConcatDictionary {get; set;}
    public TextContainer(List<TextContainerFile> files)
    {
        Files = files;
        ConcatDictionary = GenerateConcatDictionary(files.Select(x => x.FrequencyDictionaryForThisFile).ToList());
    }

    public static FrequencyDictionary GenerateConcatDictionary(List<FrequencyDictionary> dicts)
    {
        var overallFreq = new FrequencyDictionary();

        foreach(var dict in dicts)
            overallFreq.ApplyFrequencyWords(dict.Words);

        overallFreq.CalculateDifficultyScores();
        return overallFreq;
    }
}
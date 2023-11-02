

public class FrequencyDictionary 
{
    public List<FrequencyWord> Words {get; set;}
    public ulong OverallWordCount {get; set;}

    public FrequencyDictionary() => Words = new();
    public FrequencyDictionary(List<FrequencyWord> words)
    {
        Words = words; 
    }
    public void CalculateDifficultyScores()
    {
        foreach(var word in Words)
            word.DifficultyScore = Math.Log(Convert.ToDouble((OverallWordCount / word.FrequencyOfWord) * 12) / DifficultyEvaluatorService.READING_LEVEL_FACTOR, 2.0d); //Log 
    }
    public void ApplyFrequencyWords(List<FrequencyWord> words)
    {
        var toIterate = words.ConvertAll(x => new FrequencyWord(x.Word, x.Language, x.FrequencyOfWord));
        while(toIterate.Count != 0)
        {
            var word = toIterate.First();
            var toChange = Words.FirstOrDefault(x => x.Word == word.Word);
            if(toChange == null)
                Words.Add(word);
            else 
                toChange.FrequencyOfWord += word.FrequencyOfWord;
            OverallWordCount += word.FrequencyOfWord;
            toIterate.RemoveAll(x => string.Equals(x.Word, word.Word));
        }

    }

}
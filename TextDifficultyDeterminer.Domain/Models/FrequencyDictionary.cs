

public class FrequencyDictionary 
{
    public List<FrequencyWord> Words {get; set;}

    public FrequencyDictionary() => Words = new();
    public FrequencyDictionary(List<FrequencyWord> words)
    {
        Words = words; 
        CalculateRanks();
    }
    public void ApplyFrequencyWords(List<FrequencyWord> words)
    {
        foreach(var word in words)
        {
            var toChange = Words.FirstOrDefault(x => x.Word == word.Word);
            if(toChange == null)
                Words.Add(word);
            else 
                toChange.FrequencyOfWord += word.FrequencyOfWord;
        }

    }

    public void CalculateRanks()
    {
        long i = 1;
        Words = Words.OrderBy(x => x.FrequencyOfWord).ToList();
        foreach(var word in Words)
        {
            word.Rank = i;
            i++;
        }
    }
}
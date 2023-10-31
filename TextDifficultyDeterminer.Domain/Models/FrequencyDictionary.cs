

public class FrequencyDictionary 
{
    public List<FrequencyWord> Words {get; set;}


    public FrequencyDictionary() => Words = new();
    public FrequencyDictionary(List<FrequencyWord> words)
    {
        Words = words; 
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

}
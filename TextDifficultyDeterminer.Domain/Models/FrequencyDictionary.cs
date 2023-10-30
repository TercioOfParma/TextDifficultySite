

public class FrequencyDictionary 
{
    public List<FrequencyWord> Words {get; set;}

    public FrequencyDictionary(List<FrequencyWord> words)
    {
        Words = words; 
    }

    public void calculateRanks()
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
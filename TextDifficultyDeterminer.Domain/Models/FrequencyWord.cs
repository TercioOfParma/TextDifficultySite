

public class FrequencyWord 
{
    public long FrequencyWordId {get; set;}
    public int Language {get; set;}
    public string Word {get; set;}
    public ulong FrequencyOfWord {get; set;}
    public long Rank {get; set;}

    public FrequencyWord(string word, int language, ulong frequencyOfWord)
    {
        Word = word;
        Language = language;
        FrequencyOfWord = frequencyOfWord;
    }
}
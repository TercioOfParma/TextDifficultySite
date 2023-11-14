

public class FrequencyWord 
{
    public Guid FrequencyWordId {get; set;}
    public Guid Language {get; set;}
    public string Word {get; set;}
    public ulong FrequencyOfWord {get; set;}
    public double DifficultyScore {get; set;}

    public FrequencyWord()
    {
        
    }
    public FrequencyWord(string word, Guid language, ulong frequencyOfWord)
    {
        Word = word;
        FrequencyWordId = Guid.NewGuid();
        Language = language;
        FrequencyOfWord = frequencyOfWord;
    }
}
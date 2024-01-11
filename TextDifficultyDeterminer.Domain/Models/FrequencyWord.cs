using System.ComponentModel.DataAnnotations;

public class FrequencyWord 
{
    [MaxLength(255)]
    public Guid FrequencyWordId {get; set;}
    [MaxLength(255)]
    public Guid Language {get; set;}
    public string Word {get; set;}
    public long FrequencyOfWord {get; set;}
    public long DifficultyScore {get; set;}

    public FrequencyWord()
    {
        
    }
    public FrequencyWord(string word, Guid language, long frequencyOfWord)
    {
        Word = word;
        FrequencyWordId = Guid.NewGuid();
        Language = language;
        FrequencyOfWord = frequencyOfWord;
    }
}
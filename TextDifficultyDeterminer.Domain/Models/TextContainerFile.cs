
using System.Security.Cryptography.X509Certificates;

public class TextContainerFile 
{
    public string Name {get; set;}
    public string FileContents {get; set;}
    public FrequencyDictionary FrequencyDictionaryForThisFile {get; set;}
    public TextScores Scores {get; set;}
    public Guid Language {get; set;}

    public TextContainerFile(string name, string fileContents)
    {
        Name = name;
        FileContents =  new string(fileContents.Select(c => char.IsPunctuation(c) || char.IsWhiteSpace(c) ? ' ' : c).ToArray());
        Language = Guid.NewGuid();
        GenerateFrequencyDictionary(); 
    }
    public TextContainerFile(string name, string fileContents, Guid languageId)
    {
        Name = name;
        FileContents =  new string(fileContents.Select(c => char.IsPunctuation(c) || char.IsWhiteSpace(c) ? ' ' : c).ToArray());
        Language = languageId;
        GenerateFrequencyDictionary(); 
    }
    public TextContainerFile(string name, string fileContents, FrequencyDictionary dict, Guid languageId)
    {
        Name = name;
        FileContents =  new string(fileContents.Select(c => char.IsPunctuation(c) || char.IsWhiteSpace(c) ? ' ' : c).ToArray());
        Language = languageId;
        FrequencyDictionaryForThisFile = new FrequencyDictionary{ 
            OverallWordCount = dict.OverallWordCount, 
            Words = dict.Words.ConvertAll(x => new FrequencyWord { 
                DifficultyScore = x.DifficultyScore, 
                FrequencyOfWord = x.FrequencyOfWord,
                FrequencyWordId = x.FrequencyWordId,
                Language = x.Language,
                Word = x.Word
            })
        };
    }

    public void GenerateScore(FrequencyDictionary toRankAgainst) => Scores = DifficultyEvaluatorService.GenerateScore(this, toRankAgainst);
    public void OutputInformation() => FrequencyDictionaryForThisFile.Words.ForEach(x => Console.WriteLine(x.Word + " : " + x.FrequencyOfWord));


    private void GenerateFrequencyDictionary()
    {
        var toLoop = new List<string>(FileContents.ToLower().Split(" "));
        var wordList = new List<FrequencyWord>();
        var checkInstancesLength = 0;
        while(toLoop.Count != 0)
        {
            var word = toLoop.FirstOrDefault();
            if(String.IsNullOrWhiteSpace(word))
            {
                toLoop.RemoveAll(x => string.Equals(x, word));
                continue;
            }
            var numberInstances = toLoop.Where(x => string.Equals(x, word)).ToList().Count;
            checkInstancesLength += numberInstances;
            wordList.Add(new FrequencyWord(word, Language, numberInstances));

            toLoop.RemoveAll(x => string.Equals(x, word));
        }
        FrequencyDictionaryForThisFile = new(wordList);
    }
}
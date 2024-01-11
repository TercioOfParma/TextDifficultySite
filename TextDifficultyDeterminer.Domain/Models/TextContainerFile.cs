
public class TextContainerFile 
{
    public string Name {get; set;}
    public string FileContents {get; set;}
    public FrequencyDictionary FrequencyDictionaryForThisFile {get; set;}
    public TextScores Scores {get; set;}

    public TextContainerFile(string name, string fileContents)
    {
        Name = name;
        FileContents =  new string(fileContents.Select(c => char.IsPunctuation(c) || char.IsWhiteSpace(c) ? ' ' : c).ToArray());
        GenerateFrequencyDictionary(); 
    }

    public void GenerateScore(FrequencyDictionary toRankAgainst) => Scores = DifficultyEvaluatorService.GenerateScore(Name, FileContents, toRankAgainst);
    public void OutputInformation()
    {
        foreach(var word in FrequencyDictionaryForThisFile.Words)
        {
            Console.WriteLine(word.Word + " : " + word.FrequencyOfWord);
        }
    }

    private void GenerateFrequencyDictionary()
    {
        var toLoop = new List<string>(FileContents.ToLower().Split(" "));
        var wordList = new List<FrequencyWord>();
        long checkInstancesLength = 0;
        while(toLoop.Count != 0)
        {
            var word = toLoop.FirstOrDefault();
            if(word.Length != 0)
            {
                long numberInstances = Convert.ToInt64(toLoop.Where(x => string.Equals(x, word)).ToList().Count);
                checkInstancesLength += numberInstances;
                wordList.Add(new FrequencyWord(word, Guid.NewGuid(), numberInstances));
            }
            toLoop.RemoveAll(x => string.Equals(x, word));
        }
        FrequencyDictionaryForThisFile = new(wordList);
        Console.WriteLine($"In GenerateFrequencyDictionary Instances Length: {checkInstancesLength} Overall Word Count {FrequencyDictionaryForThisFile.OverallWordCount}");

    }
}

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
        while(toLoop.Count != 0)
        {
            var word = toLoop.FirstOrDefault();
            if(word.Length != 0)
            {
                ulong numberInstances = Convert.ToUInt64(toLoop.Where(x => string.Equals(x, word)).ToList().Count);
                wordList.Add(new FrequencyWord(word, 1, numberInstances));
            }
            toLoop.RemoveAll(x => x == word);
        }
        FrequencyDictionaryForThisFile = new(wordList);

    }
}
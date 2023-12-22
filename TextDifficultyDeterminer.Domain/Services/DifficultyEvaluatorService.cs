

public class DifficultyEvaluatorService 
{
    public const double FREQUENCY_MULTIPLIER = 12.0;
    private const double REALISTIC_READING_THRESHOLD = 0.95;
    private const double EXTENSIVE_READING_THRESHOLD = 0.98;
    public static TextScores GenerateScore(string name, string text, FrequencyDictionary dictToCheck)
    {
        var score = new TextScores();
        var edited = new TextContainerFile(name, text);
        var wordList = new List<FrequencyWord>();
        var wordCount = 0;
        var uniqueWords = 0;
        while(edited.FrequencyDictionaryForThisFile.Words.Count != 0)
        {
            var word = edited.FrequencyDictionaryForThisFile.Words.First();
            wordCount += Convert.ToInt32(word.FrequencyOfWord);
            uniqueWords += 1;
            var dictEntry = dictToCheck.Words.FirstOrDefault(x => x.Word == word.Word);
            word.DifficultyScore = (word.FrequencyOfWord / dictToCheck.OverallWordCount) * FREQUENCY_MULTIPLIER;
            
            wordList.Add(word);
            edited.FrequencyDictionaryForThisFile.Words.RemoveAll(x => x.Word == word.Word);
        }

        wordList = wordList.OrderBy(x => x.DifficultyScore).ToList();

        ulong realisticThreshold = Convert.ToUInt64(Math.Round(wordCount * REALISTIC_READING_THRESHOLD));
        ulong extensiveThreshold = Convert.ToUInt64(Math.Round(wordCount * EXTENSIVE_READING_THRESHOLD));


        int realisticIndex = FindIndexForThreshold(realisticThreshold, wordList);
        int extensiveIndex = FindIndexForThreshold(extensiveThreshold, wordList);
        score.Name = name; 
        score.UniqueWords = uniqueWords;
        score.WordCount = wordCount; 
        score.RealisticReadingThreshold = realisticIndex;
        score.ExtendedReadingThreshold = extensiveIndex; 
        Console.WriteLine($"{name} Difficulty Score generated!");
        return score; 
    }

    public static int FindIndexForThreshold(ulong threshold, List<FrequencyWord> words)
    {
        ulong wordCountIterator = 0;
        int i = 0;
        foreach(var word in words)
        {
            if(wordCountIterator > threshold)
                break;
            
            wordCountIterator += word.FrequencyOfWord;
            i++;
        }
        return i;
    }

}
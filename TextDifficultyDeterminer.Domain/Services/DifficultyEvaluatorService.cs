

public class DifficultyEvaluatorService 
{
    public const long FREQUENCY_MULTIPLIER = 12;
    private const double REALISTIC_READING_THRESHOLD = 0.95;
    private const double EXTENSIVE_READING_THRESHOLD = 0.98;
    public static TextScores GenerateScore(TextContainerFile file, FrequencyDictionary dictToCheck)
    {
        var score = new TextScores();
        var edited = new TextContainerFile(file.Name, file.FileContents, file.FrequencyDictionaryForThisFile, file.Language);
        var wordList = new List<FrequencyWord>();
        long wordCount = 0;
        var uniqueWords = 0;
        while(edited.FrequencyDictionaryForThisFile.Words.Count != 0)
        {
            var word = edited.FrequencyDictionaryForThisFile.Words.First();
            wordCount += word.FrequencyOfWord;
            uniqueWords += 1;
            var dictEntry = dictToCheck.Words.FirstOrDefault(x => x.Word == word.Word);
            if(dictEntry != null)
                word.DifficultyScore = dictToCheck.OverallWordCount / dictEntry.FrequencyOfWord * FREQUENCY_MULTIPLIER;
            else
                word.DifficultyScore = dictToCheck.OverallWordCount * FREQUENCY_MULTIPLIER;  

            wordList.Add(word);
            edited.FrequencyDictionaryForThisFile.Words.RemoveAll(x => x.Word == word.Word);
        }
        edited.FrequencyDictionaryForThisFile.Words = wordList;
        wordList = wordList.OrderBy(x => x.DifficultyScore).ToList();

        long realisticThreshold = Convert.ToInt64(wordCount * REALISTIC_READING_THRESHOLD);//Convert rounds for you
        long extensiveThreshold = Convert.ToInt64(wordCount * EXTENSIVE_READING_THRESHOLD);


        var realisticIndex = FindWordForThreshold(realisticThreshold, wordList);
        var extensiveIndex = FindWordForThreshold(extensiveThreshold, wordList);
        score.Name = file.Name; 
        score.UniqueWords = uniqueWords;
        score.WordCount = wordCount; 
        score.RealisticReadingThreshold = Convert.ToInt32(realisticIndex.DifficultyScore);
        score.ExtendedReadingThreshold = Convert.ToInt32(extensiveIndex.DifficultyScore); 
        Console.WriteLine($"{file.Name} Difficulty Score generated!");
        return score; 
    }

    public static FrequencyWord FindWordForThreshold(long threshold, List<FrequencyWord> words)
    {
        long wordCountIterator = 0;
        var currentWord = new FrequencyWord();
        foreach(var word in words)
        {
            if(wordCountIterator > threshold)
                break;
            
            wordCountIterator += word.FrequencyOfWord;
            currentWord = word;
        }
        return currentWord;
    }

}


public class DifficultyEvaluatorService 
{
    private const double READING_LEVEL_FACTOR = 125_000.0;
    private const double REALISTIC_READING_THRESHOLD = 0.95;
    private const double EXTENSIVE_READING_THRESHOLD = 0.98;
    public static TextScores GenerateScore(string name, string text, FrequencyDictionary dictToCheck)
    {
        var score = new TextScores();
        var edited = new TextContainerFile(text);
        var wordList = new List<FrequencyWord>();
        var wordCount = 0;
        var uniqueWords = 0;
        while(edited.FrequencyDictionaryForThisFile.Words.Count != 0)
        {
            var word = edited.FrequencyDictionaryForThisFile.Words.First();
            wordCount += Convert.ToInt32(word.FrequencyOfWord);
            uniqueWords += 1;
            var dictEntry = dictToCheck.Words.FirstOrDefault(x => x.Word == word.Word);
            if(dictEntry != null)
                word.DifficultyScore = dictEntry.DifficultyScore;
            else 
                word.DifficultyScore = Math.Log(2.0d, Convert.ToDouble(word.FrequencyOfWord) / READING_LEVEL_FACTOR); //Log 
            wordList.Add(word);
            edited.FrequencyDictionaryForThisFile.Words.RemoveAll(x => x.Word == word.Word);
        }

        wordList = wordList.OrderBy(x => x.DifficultyScore).ToList();
        int realisticIndex = Convert.ToInt32((wordList.Count - 1) * REALISTIC_READING_THRESHOLD);
        int extensiveIndex = Convert.ToInt32((wordList.Count - 1) * EXTENSIVE_READING_THRESHOLD);

        score.Name = name; 
        score.UniqueWords = uniqueWords;
        score.WordCount = wordCount; 
        score.RealisticReadingThreshold = Convert.ToInt32(Math.Pow(2.0, wordList.ElementAt(realisticIndex).DifficultyScore));
        score.ExtendedReadingThreshold = Convert.ToInt32(Math.Pow(2.0, wordList.ElementAt(extensiveIndex).DifficultyScore)); 

        return score; 
    }

}
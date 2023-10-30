
public class ZipContainerFile 
{
    public string FileContents {get; set;}
    public FrequencyDictionary FrequencyDictionaryForThisFile {get; set;}

    public ZipContainerFile(string fileContents)
    {
        FileContents =  new string(fileContents.Select(c => char.IsPunctuation(c) ? ' ' : c).ToArray());
        GenerateFrequencyDictionary(); 
    }

    private void GenerateFrequencyDictionary()
    {
        var toLoop = FileContents.Split(" ");
        var wordList = new List<FrequencyWord>();
        foreach(var word in toLoop)
        {
            if(wordList.Where(x => string.Equals(word, x.Word)).ToList().Count == 0)
            {
                var newWord = new FrequencyWord(word, 1, 1);
                wordList.Add(newWord);
            }
            else 
            {
                var toAdd = wordList.Where(x => string.Equals(word, x.Word)).FirstOrDefault();
                if(toAdd != null)
                    toAdd.FrequencyOfWord += 1;
            }
        }
        FrequencyDictionaryForThisFile = new(wordList);

    }
}
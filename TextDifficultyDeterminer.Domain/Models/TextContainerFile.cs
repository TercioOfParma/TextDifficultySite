
public class TextContainerFile 
{
    public string FileContents {get; set;}
    public FrequencyDictionary FrequencyDictionaryForThisFile {get; set;}

    public TextContainerFile(string fileContents)
    {
        FileContents =  new string(fileContents.Select(c => char.IsPunctuation(c) || c == '\n' || c == '\r' ? ' ' : c).ToArray());
        Console.WriteLine("Generating Frequencies!");
        GenerateFrequencyDictionary(); 
        OutputInformation();
    }

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
            ulong numberInstances = Convert.ToUInt64(toLoop.Where(x => string.Equals(x, word)).ToList().Count);
            wordList.Add(new FrequencyWord(word, 1, numberInstances));
            toLoop.RemoveAll(x => x == word);
            Console.WriteLine($"{toLoop.Count}");
        }
        FrequencyDictionaryForThisFile = new(wordList);

    }
}
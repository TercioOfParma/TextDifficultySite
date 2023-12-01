using Microsoft.EntityFrameworkCore;

public static class LoadIntoDatabaseService
{

    public static async Task LoadTextContainerFileIntoDatabase(TextContainerFile file, Guid LanguageId, IUnitOfWork db)
    {
        var relevantWords = await db.Words.Where(x => x.Language == LanguageId).ToListAsync();
        var newWords = new List<FrequencyWord>();
        foreach(var word in file.FrequencyDictionaryForThisFile.Words)
        {
            Console.WriteLine(word.Word);
            var toModify = relevantWords.Where(x => x.Word == word.Word).FirstOrDefault();
            if(toModify != null)
                toModify.FrequencyOfWord += word.FrequencyOfWord;
            else
                newWords.Add(word);
            
        }
        await db.Words.AddRangeAsync(newWords);
        await db.SaveChangesAsync();
    }
}
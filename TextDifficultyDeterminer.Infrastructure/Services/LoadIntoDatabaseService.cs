using Microsoft.EntityFrameworkCore;

public static class LoadIntoDatabaseService
{

    public static async Task LoadTextContainerFileIntoDatabase(List<TextContainerFile> files, Guid LanguageId, IUnitOfWork db)
    {
        var relevantWords = await db.Words.Where(x => x.Language == LanguageId).ToListAsync();
        var newWords = new List<FrequencyWord>();
        foreach(var file in files)
        {
            Console.WriteLine(file.Name);
            foreach(var word in file.FrequencyDictionaryForThisFile.Words)
            {
                var InDb = relevantWords.Where(x => x.Word == word.Word).FirstOrDefault();
                if(InDb != null)
                {
                    InDb.FrequencyOfWord += word.FrequencyOfWord;
                    continue;
                }
                var InNew = newWords.Where(x => x.Word == word.Word).FirstOrDefault();
                if(InNew != null)
                    InNew.FrequencyOfWord += word.FrequencyOfWord;
                else
                    newWords.Add(word);
            }
        }
        await db.Words.AddRangeAsync(newWords);
        await db.SaveChangesAsync();
    }
}
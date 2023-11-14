public class Language 
{
    public Guid Id {get; set;}
    public string LanguageName {get; set;}

    public Language()
    {
        
    }
    public Language(string name)
    {
        Id = Guid.NewGuid();
        LanguageName = name;
    }
}
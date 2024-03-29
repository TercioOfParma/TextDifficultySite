using System.ComponentModel.DataAnnotations;

public class Language 
{
    [MaxLength(255)]
    public Guid Id {get; set;}
    public string LanguageName {get; set;}

    public Language()
    {
        Id = Guid.NewGuid();
    }
    public Language(string name)
    {
        Id = Guid.NewGuid();
        LanguageName = name;
    }
}
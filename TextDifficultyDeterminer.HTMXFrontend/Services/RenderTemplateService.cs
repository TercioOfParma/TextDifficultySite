using Scriban;

public static class RenderTemplateService 
{
    public static Template RenderTemplate(string path)
    {
        var index = System.IO.File.ReadAllText(path);
        return Template.Parse(index);
    }
}
using Scriban;
using Scriban.Parsing;
public class TemplateLoaderService : Scriban.Runtime.ITemplateLoader 
{
    public string GetPath(TemplateContext context, SourceSpan callerSpan, string templateName)
    {
        return Path.Combine(Environment.CurrentDirectory + "/Templates", templateName);
    }

    public string Load(TemplateContext context, SourceSpan callerSpan, string templatePath)
    {
        // Template path was produced by the `GetPath` method above in case the Template has 
        // not been loaded yet
        return File.ReadAllText(templatePath);
    }
    public async ValueTask<string> LoadAsync(TemplateContext context, SourceSpan callerSpan, string templatePath)
    {
        // Template path was produced by the `GetPath` method above in case the Template has 
        // not been loaded yet
        return File.ReadAllText(templatePath);
    }
}
using Microsoft.AspNetCore.Mvc;
using Scriban;

namespace TextDifficultyDeterminer.HTMXFrontend.Controllers 
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("/")]
    public class HTMXFrontendController : ControllerBase
    {
        public Template IndexTemplate {get; set;}
        public Template CreateLanguageTemplate {get; set;}
        public TemplateContext Context {get; set;}
        public HTMXFrontendController(Scriban.Runtime.ITemplateLoader templateLoader)
        {
            Context = new TemplateContext();
            Context.TemplateLoader = templateLoader;
            IndexTemplate = RenderTemplateService.RenderTemplate("Templates/Index.html");
            CreateLanguageTemplate = RenderTemplateService.RenderTemplate("Templates/CreateLanguage.html");
        }
        
        [HttpGet("/")]
        public ContentResult LoadIndex()
        {
            //await Task.CompletedTask;
            return new ContentResult 
            {   Content = IndexTemplate.Render(Context), 
                ContentType = "text/html"
            };
        }
        [HttpGet("/CreateLanguage")]
        public ContentResult CreateLanguage()
        {
            //await Task.CompletedTask;
            return new ContentResult
            { 
                Content = CreateLanguageTemplate.Render(Context), 
                ContentType = "text/html"
            };
        }
        [HttpPost("/CreateLanguage")]
        [Consumes("application/x-www-form-urlencoded")]
        public ContentResult CreateLanguage([FromForm]string LanguageName)
        {
            //await Task.CompletedTask;
            return new ContentResult
            { 
                Content = "<p>Complete!</p>", 
                ContentType = "text/html"
            };
        }
    }
}
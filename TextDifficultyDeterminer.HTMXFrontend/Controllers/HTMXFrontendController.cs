using Microsoft.AspNetCore.Mvc;
using Scriban;

namespace TextDifficultyDeterminer.HTMXFrontend.Controllers 
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("/")]
    public class HTMXFrontendController : ControllerBase
    {
        public Template IndexTemplate {get; set;}
        public TemplateContext Context {get; set;}
        public HTMXFrontendController(Scriban.Runtime.ITemplateLoader templateLoader)
        {
            Context = new TemplateContext();
            Context.TemplateLoader = templateLoader;
            var index = System.IO.File.ReadAllText("Templates/index.html");
            IndexTemplate = Template.Parse(index);
        }
        [HttpGet("/")]
        public IActionResult LoadIndex()
        {
            //await Task.CompletedTask;
            return base.Content(IndexTemplate.Render(Context), "text/html");
        }
        [HttpGet("/CreateLanguage")]
        public ContentResult CreateLanguage()
        {
            //await Task.CompletedTask;
            return new ContentResult
            { 
                Content = "<h1> Create Language </h1>", 
                ContentType = "text/html"
            };
        }
    }
}
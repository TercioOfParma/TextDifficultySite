using Microsoft.AspNetCore.Mvc;
using Scriban;
using MediatR;
//using TextDifficultyDeterminer.Application;
//using TextDifficultyDeterminer.Domain;

namespace TextDifficultyDeterminer.HTMXFrontend.Controllers 
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("/")]
    public class HTMXFrontendController : ControllerBase
    {
        public Template IndexTemplate {get; set;}
        public Template CreateLanguageTemplate {get; set;}
        public TemplateContext Context {get; set;}
        public IMediator _mediator {get; set;}
        public HTMXFrontendController(Scriban.Runtime.ITemplateLoader templateLoader, IMediator mediator)
        {
            Context = new TemplateContext();
            _mediator = mediator;
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
        public async Task<ContentResult> CreateLanguage([FromForm]string LanguageName)
        {
            var result = await _mediator.Send(new AddLanguageCommand { Language = new Language { LanguageName = LanguageName }});
            return new ContentResult
            { 
                Content = "<p>Complete!</p>", 
                ContentType = "text/html"
            };
        }
    }
}
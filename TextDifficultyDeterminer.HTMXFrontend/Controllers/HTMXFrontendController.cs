using Microsoft.AspNetCore.Mvc;
using Scriban;
using Scriban.Runtime;
using MediatR;

namespace TextDifficultyDeterminer.HTMXFrontend.Controllers 
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("/")]
    public class HTMXFrontendController : ControllerBase
    {
        public Template IndexTemplate {get; set;}
        public Template CreateLanguageTemplate {get; set;}
        public Template LoadFilesIntoLanguageTemplate {get; set;}
        public TemplateContext Context {get; set;}
        public IMediator _mediator {get; set;}
        public HTMXFrontendController(Scriban.Runtime.ITemplateLoader templateLoader, IMediator mediator)
        {
            Context = new TemplateContext();
            _mediator = mediator;
            Context.TemplateLoader = templateLoader;
            IndexTemplate = RenderTemplateService.RenderTemplate("Templates/Index.html");
            CreateLanguageTemplate = RenderTemplateService.RenderTemplate("Templates/CreateLanguage.html");
            LoadFilesIntoLanguageTemplate = RenderTemplateService.RenderTemplate("Templates/LoadNewFilesIntoLanguage.html");
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
            if(result)
                {
                return new ContentResult
                { 
                    Content = "<p>Complete!</p>", 
                    ContentType = "text/html"
                };
            }
            else
            {
                return new ContentResult
                { 
                    Content = "<p>Error, Language Already Exists!</p>", 
                    ContentType = "text/html"
                };
            }
        }
        [HttpGet("/LoadNewFilesIntoLanguage")]
        public async Task<ContentResult> LoadNewFilesIntoLanguageForm()
        {
            var languages = await _mediator.Send(new GetLanguagesQuery());
            var parameters = new ScriptObject();
            parameters["Languages"] = languages.LanguageList;
            Context.PushGlobal(parameters);
            var content = LoadFilesIntoLanguageTemplate.Render(Context);
            Context.PopGlobal();
            return new ContentResult
            { 
                Content = content, 
                ContentType = "text/html"
            };
        }
    }
}
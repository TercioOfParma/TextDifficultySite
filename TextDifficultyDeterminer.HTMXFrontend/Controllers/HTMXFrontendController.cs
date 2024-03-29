using Microsoft.AspNetCore.Mvc;
using System.Web;
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
        public Template CheckFilesAgainstDatabaseTemplate {get; set;}
        public Template NonAjax {get; set;}
        public Template DownloadTemplate {get; set;}
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
            CheckFilesAgainstDatabaseTemplate = RenderTemplateService.RenderTemplate("Templates/CheckAgainstDb.html");
            DownloadTemplate = RenderTemplateService.RenderTemplate("Templates/Download.html");
            NonAjax = RenderTemplateService.RenderTemplate("Templates/NonAjax.html");
        }
        
        [HttpGet("/")]
        public ContentResult LoadIndex()
        {
            var parameters = new ScriptObject();
            var content ="";
            if(HttpContext.Request.Headers["HX-Request"].Count() == 0)
                content = RenderNonAjaxPage("CreateLanguage.html", parameters);
            else 
                content = IndexTemplate.Render(Context);
            return new ContentResult 
            {   Content = content, 
                ContentType = "text/html"
            };
        }
        [HttpGet("/language")]
        public ContentResult CreateLanguage()
        {
            var parameters = new ScriptObject();
            var content ="";
            if(HttpContext.Request.Headers["HX-Request"].Count() == 0)
                content = RenderNonAjaxPage("CreateLanguage.html", parameters);
            else 
                content = CreateLanguageTemplate.Render(Context);
            return new ContentResult
            { 
                Content = content, 
                ContentType = "text/html"
            };
        }
        [HttpPost("/language")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ContentResult> CreateLanguage([FromForm]string LanguageName)
        {
            var result = await _mediator.Send(new AddLanguageCommand { Language = new Language { LanguageName = LanguageName }});
            if(result)
                {
                    HttpContext.Response.Headers.Add("HX-Trigger", "new-language-created");
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
        [HttpGet("/load")]
        public async Task<ContentResult> LoadNewFilesIntoLanguageForm()
        {
            var languages = await _mediator.Send(new GetLanguagesQuery());
            var parameters = new ScriptObject();
            parameters["Languages"] = languages.LanguageList;
            var content ="";
            if(HttpContext.Request.Headers["HX-Request"].Count() == 0)
                content = RenderNonAjaxPage("LoadNewFilesIntoLanguage.html", parameters);
            else
            {
                Context.PushGlobal(parameters);
                content = LoadFilesIntoLanguageTemplate.Render(Context);
            }
            Context.PopGlobal();
            return new ContentResult
            { 
                Content = content, 
                ContentType = "text/html"
            };
        }

        [HttpPost("/load")]
        public async Task<ContentResult> LoadNewFilesIntoLanguageForm([FromForm] List<IFormFile> Files, [FromForm] Guid Language)
        {
            var result = true;
            Dictionary<string, string> dict = new();
            var numberOfTokens = 0;
            System.Diagnostics.Debug.WriteLine(Files.Count);
            foreach(var file in Files)
            {
                if(file.ContentType != "text/plain")
                    continue;
                var reader = new StreamReader(file.OpenReadStream());
                var textForFile = reader.ReadToEnd();
                dict[file.FileName] = textForFile;
                numberOfTokens += textForFile.Length;
                System.Diagnostics.Debug.WriteLine($"{numberOfTokens}");

            }
            await _mediator.Send(new LoadFileIntoDatabaseCommand { FilesAndFilenames = dict,LanguageId = Language});
            if(result)
                {
                var complete = new ContentResult
                { 
                    Content = $"<p>Complete! Tokens: {numberOfTokens} Number of Files = {Files.Count} GUID = {Language}</p>", 
                    ContentType = "text/html"
                };
                return complete;
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
        private string RenderNonAjaxPage(string filename, ScriptObject parameters)
        {
            parameters["FileName"] = filename;
            Context.PushGlobal(parameters);
            return NonAjax.Render(Context);
        }

        [HttpGet("/check")]
        public async Task<ContentResult> CheckFilesAgainstDatabasePage()
        {
            var languages = await _mediator.Send(new GetLanguagesQuery());
            var parameters = new ScriptObject();
            parameters["Languages"] = languages.LanguageList;
            var content = "";
            if(HttpContext.Request.Headers["HX-Request"].Count() == 0)
                content = RenderNonAjaxPage("CheckAgainstDb.html", parameters);
            else 
            {
                Context.PushGlobal(parameters);
                content = CheckFilesAgainstDatabaseTemplate.Render(Context);
            }
            Context.PopGlobal();
            return new ContentResult
            { 
                Content = content, 
                ContentType = "text/html"
            };
        }
        [HttpPost("/check")]
        public async Task<ActionResult> CheckFilesAgainstDatabase([FromForm] List<IFormFile> Files, [FromForm] Guid Language)
        {
            var result = true;
            Dictionary<string, string> dict = new();
            var numberOfTokens = 0;
            System.Diagnostics.Debug.WriteLine(Files.Count);
            foreach(var file in Files)
            {
                if(file.ContentType != "text/plain")
                    continue;
                var reader = new StreamReader(file.OpenReadStream());
                var textForFile = reader.ReadToEnd();
                dict[file.FileName] = textForFile;
                numberOfTokens += textForFile.Length;
                System.Diagnostics.Debug.WriteLine($"{numberOfTokens}");

            }
            var container = await _mediator.Send(new CheckTextAgainstDbQuery { LanguageId = Language, Files = dict});
            var excelFile = await _mediator.Send(new TextContainerToExcelCommand { Container = container.Text});
            var stream = new MemoryStream();
            excelFile.SaveAs(stream);
            stream.Position = 0;
            if(result)
            {
                HttpContext.Response.Headers.Add("HX-Trigger", "file-finished");
                HttpContext.Response.Headers.Add("Content-Disposition", "attachment; filename=\"Result.xlsx\"");
                var complete = new FileContentResult(stream.ToArray(), "application/octet-stream");
                return complete;
            }
            else
            {
                return new ContentResult
                { 
                    Content = "<p>Error!</p>", 
                    ContentType = "text/html"
                };
            }
        }
        [HttpGet("downloadscript")]
        public async Task<ContentResult> ReturnDownloadInfo()
        {
            var content = DownloadTemplate.Render(Context);
            return new ContentResult
            { 
                Content = content, 
                ContentType = "text/html"
            };
        }
    }
}
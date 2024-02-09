using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
namespace TextDifficultyDeterminer.HTMXFrontend.Controllers 
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("/")]
    public class StaticController : ControllerBase
    {
        [HttpGet("/static/{fileName}")]
        public async Task<IActionResult> GetStaticFile(string fileName)
        {
            try 
            {
                var result = System.IO.File.ReadAllText($"static/{fileName}");
                var provider = new FileExtensionContentTypeProvider();
                var contentType = "";
                if (!provider.TryGetContentType($"static/{fileName}", out contentType))
                {
                    contentType = "text/css";
                }
                Console.WriteLine(contentType);
                return new ContentResult{ Content = result, ContentType = contentType};
            }
            catch(Exception e)
            {
                return new ContentResult{Content= $"<p>{fileName} not found!", ContentType = "text/html"};
            }
        }
    }
}
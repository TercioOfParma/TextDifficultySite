using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;


namespace TextDifficultyDeterminer.Website.Controllers 
{
    [Microsoft.AspNetCore.Mvc.Route("api/")]
    [ApiController]
    public class TextCorpusControllerApi : ControllerBase
    {
        protected IMediator Mediator {get; set;}
        public IWebHostEnvironment WebHostEnvironment {get; set;}

        public TextCorpusControllerApi(IMediator mediator, IWebHostEnvironment webHostEnvironment)
        {
            Mediator = mediator;
            WebHostEnvironment = webHostEnvironment;
        }
        [HttpPost("LoadCorpus/{LanguageId}")]
        public async Task<IActionResult> LoadFilesIntoDatabase(Guid LanguageId, IList<IFormFile> files)
        {
            Dictionary<string, string> dict = new();
            foreach(var file in files)
            {
                if(file.ContentType != "text/plain")
                    continue;
                var reader = new StreamReader(file.OpenReadStream());
                var textForFile = reader.ReadToEnd();
                dict[file.FileName] = textForFile;
            }
            await Mediator.Send(new LoadFileIntoDatabaseCommand { FilesAndFilenames = dict, LanguageId = LanguageId});
            return Ok();
        }
        [HttpPost("TestText/{LanguageId}")]
        public async Task<IActionResult> CheckFilesAgainstDatabase(Guid LanguageId, IList<IFormFile> files)
        {
            Dictionary<string, string> dict = new();
            foreach(var file in files)
            {
                if(file.ContentType != "text/plain")
                    continue;
                var reader = new StreamReader(file.OpenReadStream());
                var textForFile = reader.ReadToEnd();
                dict[file.FileName] = textForFile;
            }
            var container = await Mediator.Send(new CheckTextAgainstDbQuery{ Files = dict, LanguageId = LanguageId});
            var contString = JsonSerializer.Serialize(container);
            return Ok(contString);
        }

    }

}
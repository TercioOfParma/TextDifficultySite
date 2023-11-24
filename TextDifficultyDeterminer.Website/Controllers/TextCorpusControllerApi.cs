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
            Console.WriteLine("PING");

            return Ok();
        }

        [HttpPost("LoadTestCorpus")]
        public async Task<IActionResult> LoadFiles(IList<IFormFile> files)
        {
            Console.WriteLine("PING");
            var containerList = new List<TextContainerFile>();
            foreach(var file in files)
            {
                if(file.ContentType != "text/plain")
                    continue;

                var converted = await Mediator.Send(new TextFileToTextContainerCommand { File = file});
                containerList.Add(converted);
            }
            var container = new TextContainer(containerList);
            foreach(var file in container.Files)
            {
                file.GenerateScore(container.ConcatDictionary);
                Console.WriteLine($"{file.Name} : {file.Scores.RealisticReadingThreshold}");
            }

            var contString = JsonSerializer.Serialize(container);
            Console.WriteLine("Sending Container");
            return Ok(contString);
        }

    }

}
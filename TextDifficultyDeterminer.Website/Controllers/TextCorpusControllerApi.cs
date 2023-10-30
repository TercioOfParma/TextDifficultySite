using MediatR;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("LoadCorpus")]
        public async Task<ActionResult> LoadFiles(IList<IFormFile> files)
        {
            Console.WriteLine("PING");
            var container = new List<TextContainerFile>();
            foreach(var file in files)
            {
                if(file.ContentType != "text/plain")
                    continue;

                var converted = await Mediator.Send(new TextFileToTextContainerCommand { File = file});
                container.Add(converted);
            }
            return Ok("Skull Emoji");

        }

    }

}
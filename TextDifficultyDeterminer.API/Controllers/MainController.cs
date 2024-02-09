using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
namespace TextDifficultyDeterminer.API.Controllers 
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("/")]
    public class MainController : ControllerBase
    {
        public IMediator _mediator {get; set;}
        public MainController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("/CheckText")]
        public async Task<IActionResult> CheckText(CheckReq jsonObj)
        {
            await Task.CompletedTask;
            //var jsonObj  = obj.ToObject<CheckReq>();
            var dict = new Dictionary<string, string>();
            dict["Input"] = jsonObj.Text;
            //Console.WriteLine(jsonObj.Text);
            Console.WriteLine(jsonObj.LanguageId);
            var container = await _mediator.Send(new CheckTextAgainstDbQuery { LanguageId = Guid.Parse(jsonObj.LanguageId), Files = dict});
            //var excelFile = await _mediator.Send(new TextContainerToExcelCommand { Container = container.Text});
            return Ok();
        }
    }
}
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

namespace TextDifficultyDeterminer.Website.Services 
{
    public class ProcessTextFiles
    {
        protected IMediator Mediator {get; set;}

        public ProcessTextFiles(IMediator mediator)
        {
            Mediator = mediator;
        }
        public async Task LoadFilesIntoDatabase(Guid LanguageId, Dictionary<string, string> files)
        {
            await Mediator.Send(new LoadFileIntoDatabaseCommand { FilesAndFilenames = files,LanguageId = LanguageId});
        }
        public async Task<TextContainer> CheckFilesAgainstDatabase(Guid LanguageId, Dictionary<string, string> files)
        {
            var timeBefore = DateTime.Now;
            var dictionary = (await Mediator.Send(new GetFrequencyDictionaryQuery { LanguageId = LanguageId})).Dictionary;
            var timeAfter = DateTime.Now;

            Console.WriteLine($"Time Dictionary {(timeAfter - timeBefore).TotalSeconds}");
            timeBefore = DateTime.Now;
            var container = await GenerateTextContainer(files);
            timeAfter = DateTime.Now;

            Console.WriteLine($"Time Generate Text Container {(timeAfter - timeBefore).TotalSeconds}");
            timeBefore = DateTime.Now;
            container.Files.ForEach(x => x.GenerateScore(dictionary));  
            timeAfter = DateTime.Now;

            Console.WriteLine($"Time Generate Score {(timeAfter - timeBefore).TotalSeconds}");
    
            return container; 
        }
        private async Task<TextContainer> GenerateTextContainer(Dictionary<string, string> files)
        {
            var containerList = new List<TextContainerFile>();
            foreach(var file in files)
            {
                var converted = await Mediator.Send(new TextFileToTextContainerCommand { Filename = file.Key, Text = file.Value});
                containerList.Add(converted);
            }
            return new TextContainer(containerList, false);
        }

        public async Task<TextContainer> LoadFiles(Dictionary<string, string> files)
        {
            var container = await GenerateTextContainer(files);
            container.Files.ForEach(x => x.GenerateScore(container.ConcatDictionary));  
            return container;
        }
    }
}
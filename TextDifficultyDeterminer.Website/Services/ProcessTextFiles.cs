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
        public IWebHostEnvironment WebHostEnvironment {get; set;}

        public ProcessTextFiles(IMediator mediator, IWebHostEnvironment webHostEnvironment)
        {
            Mediator = mediator;
            WebHostEnvironment = webHostEnvironment;
        }
        public async Task LoadFilesIntoDatabase(Guid LanguageId, Dictionary<string, string> files)
        {
            foreach(var file in files) 
            {
                var converted = await Mediator.Send(new LoadFileIntoDatabaseCommand { Filename = file.Key, Text = file.Value, LanguageId = LanguageId});
            }
        }
        public async Task<TextContainer> CheckFilesAgainstDatabase(Guid LanguageId, Dictionary<string, string> files)
        {
            var containerList = new List<TextContainerFile>();
            foreach(var file in files)
            {
                var converted = await Mediator.Send(new TextFileToTextContainerCommand { Filename = file.Key, Text = file.Value});
                containerList.Add(converted);
            }
            var dictionary = (await Mediator.Send(new GetFrequencyDictionaryQuery { LanguageId = LanguageId})).Dictionary;
            var container = new TextContainer(containerList, true);
            container.ConcatDictionary = dictionary;

            foreach(var file in container.Files)
            {
                file.GenerateScore(dictionary);
                Console.WriteLine($"{file.Name} : {file.Scores.RealisticReadingThreshold}");
            }            
            return container; 
        }

        public async Task<TextContainer> LoadFiles(Dictionary<string, string> files)
        {
            var containerList = new List<TextContainerFile>();
            foreach(var file in files)
            {
                var converted = await Mediator.Send(new TextFileToTextContainerCommand { Filename = file.Key, Text = file.Value});
                containerList.Add(converted);
            }
            var container = new TextContainer(containerList, false);
            foreach(var file in container.Files)
            {
                file.GenerateScore(container.ConcatDictionary);
                Console.WriteLine($"{file.Name} : {file.Scores.RealisticReadingThreshold}");
            }

            return container;
        }
    }
}
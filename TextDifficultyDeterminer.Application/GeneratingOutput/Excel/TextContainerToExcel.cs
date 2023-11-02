using MediatR;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using ClosedXML.Excel;
public class TextContainerToExcelCommand : IRequest<XLWorkbook>
{
    public TextContainer Container {get; set;}
}

public class TextContainerToExcelValidator : AbstractValidator<TextContainerToExcelCommand>
{
    public TextContainerToExcelValidator()
    {
        
    }
}

class TextContainerToExcelHandler : IRequestHandler<TextContainerToExcelCommand, XLWorkbook>
{
    async Task<XLWorkbook> IRequestHandler<TextContainerToExcelCommand, XLWorkbook>.Handle(TextContainerToExcelCommand request, CancellationToken cancellationToken)
    {
        var workbook = new XLWorkbook();
        var concatDictionary = workbook.AddWorksheet("Concatenated Dictionary");
        concatDictionary.Cell(3,1).InsertData(new List<string> {"Overall Word Count"});
        concatDictionary.Cell(3,1).InsertData(new List<ulong> {request.Container.ConcatDictionary.OverallWordCount});

        concatDictionary.Cell(4,1).InsertData(new List<string> {"Frequency Word ID", "Language", "Word", "Frequency", "DifficultyScore"}, transpose : true);
        concatDictionary.Cell(5,1).InsertData(request.Container.ConcatDictionary.Words.OrderByDescending(x => x.FrequencyOfWord).ToList());
        foreach(var file in request.Container.Files)
        {
            GenerateXLWorksheetOutput(workbook, file);
            Console.WriteLine($"{file.Name} Done");
        }

        return workbook;
    }

    private void GenerateXLWorksheetOutput(XLWorkbook workbook, TextContainerFile file)
    {
        var worksheet = workbook.AddWorksheet(file.Name);

        worksheet.Cell(1,1).InsertData(new List<string>{"Name"});
        worksheet.Cell(1,2).InsertData(new List<string>{file.Name});
        worksheet.Cell(4,1).InsertData(new List<string> {"Frequency Word ID", "Language", "Word", "Frequency", "DifficultyScore"}, transpose : true);
        worksheet.Cell(5,1).InsertData(file.FrequencyDictionaryForThisFile.Words.OrderByDescending(x => x.FrequencyOfWord).ToList());
        Console.WriteLine($"Total File Length from Generate Score {file.Scores.WordCount} From Frequency Dictionary for that File {file.FrequencyDictionaryForThisFile.Words.Select(x => Convert.ToInt32(x.FrequencyOfWord)).ToList().Sum()}");
        worksheet.Cell(4,7).InsertData(new List<string> {"Name", "Realistic Reading Threshold", "Extensive Reading Threshold", "Word Count", "Unique Words"}, transpose: true);
        worksheet.Cell(5,7).InsertData(new List<TextScores> {file.Scores });
    }
}
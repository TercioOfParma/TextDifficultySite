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
    private const int MAXIMUM_SHEETNAME_LENGTH = 31;
    async Task<XLWorkbook> IRequestHandler<TextContainerToExcelCommand, XLWorkbook>.Handle(TextContainerToExcelCommand request, CancellationToken cancellationToken)
    {
        var workbook = new XLWorkbook();
        var concatDictionary = workbook.AddWorksheet("Overall Scores");
        concatDictionary.Cell(3,1).InsertData(new List<string> {"Overall Word Count"});
        concatDictionary.Cell(3,2).InsertData(new List<long> {request.Container.ConcatDictionary.OverallWordCount});
        var textScoresForFiles = request.Container.Files.Select(x => x.Scores).OrderBy(x => x.RealisticReadingThreshold).ToList();
        concatDictionary.Cell(4,1).InsertData(new List<string> {"Name", "Realistic Reading Threshold", "Extensive Reading Threshold", "Word Count", "Unique Words"}, transpose: true);
        concatDictionary.Cell(5,1).InsertData(textScoresForFiles);
        foreach(var file in request.Container.Files)
        {
            GenerateXLWorksheetOutput(workbook, file);
            Console.WriteLine($"{file.Name} Done");
        }
        Console.WriteLine("Workbook Done");

        return workbook;
    }

    private void GenerateXLWorksheetOutput(XLWorkbook workbook, TextContainerFile file)
    {
        var filename = file.Name;
        if(file.Name.Length > MAXIMUM_SHEETNAME_LENGTH)
            filename = file.Name.Substring(0, MAXIMUM_SHEETNAME_LENGTH);

        var worksheet = workbook.AddWorksheet(filename);

        worksheet.Cell(1,1).InsertData(new List<string>{"Name"});
        worksheet.Cell(1,2).InsertData(new List<string>{file.Name});
        worksheet.Cell(4,1).InsertData(new List<string> {"Frequency Word ID", "Language", "Word", "Frequency", "DifficultyScore"}, transpose : true);
        worksheet.Cell(5,1).InsertData(file.FrequencyDictionaryForThisFile.Words.OrderByDescending(x => x.FrequencyOfWord).ToList());
        worksheet.Cell(4,7).InsertData(new List<string> {"Name", "Realistic Reading Threshold", "Extensive Reading Threshold", "Word Count", "Unique Words"}, transpose: true);
        worksheet.Cell(5,7).InsertData(new List<TextScores> {file.Scores });
    }
}
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
        foreach(var file in request.Container.Files)
        {
            GenerateXLWorksheetOutput(workbook, file);
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

        worksheet.Cell(4,7).InsertData(new List<string> {"Name", "Realistic Reading Threshold", "Extensive Reading Threshold", "Word Count", "Unique Words"}, transpose: true);
        worksheet.Cell(5,7).InsertData(new List<TextScores> {file.Scores });
    }
}
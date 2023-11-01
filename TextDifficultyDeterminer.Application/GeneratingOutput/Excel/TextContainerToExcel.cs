using MediatR;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using ClosedXML.Excel;
public class TextContainerToExcelCommand : IRequest<XLWorkbook>
{
    public TextContainer Files {get; set;}
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


        return workbook;
    }
}
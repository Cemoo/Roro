﻿using Microsoft.Office.Interop.Excel;
using System;
using System.ComponentModel;

namespace Roro.Activities.Excel
{
    public class GetCellValue : Activity
    {
        public InArgument<string> WorkbookPath { get; set; }

        public InArgument<string> WorksheetName { get; set; }

        public InArgument<int> RowIndex { get; set; }

        public InArgument<int> ColumnIndex { get; set; }

        public OutArgument<string> CellValue { get; set; }

        public override void Execute(Context context)
        {
            // inputs
            var workbookPath = context.Get(this.WorkbookPath);
            var worksheetName = context.Get(this.WorksheetName);
            var rowIndex = context.Get(this.RowIndex);
            var columnIndex = context.Get(this.ColumnIndex);
         
            var bot = ExcelBot.Shared;
            var xlApp = bot.Get(workbookPath);
            var xlWbs = xlApp.Workbooks;
            var xlWb = xlWbs.Item[1];
            var xlWss = xlWb.Worksheets;
            var xlWs = xlWss.Item[0] as Worksheet;
            var xlCell = xlWs.Cells[rowIndex, columnIndex] as Range;
            var cellValue = (xlCell.Value ?? string.Empty).ToString();
            bot.Release(xlCell, xlWs, xlWss, xlWb, xlWbs);

            // outputs
            context.Set(this.CellValue, cellValue);
        }
    }
}

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lab1.Models;
using OfficeOpenXml;

namespace Lab1.Logger
{
    public static class ExcelLogger
    {
        public static void LogInterval(List<(CountableFunc, string)> funcData, string filePath = "intervals.xlsx")
        {
            var package = new ExcelPackage();

            foreach (var tuple in funcData)
            {
                var ws = package.Workbook.Worksheets.Add(tuple.Item2);
                ws.Cells[1, 1].Value = "Interval";
                ws.Cells[1, 2].Value = "Ratio";
                ws.Cells[1, 3].Value = "Left value";
                ws.Cells[1, 4].Value = "Right vale";

                for (var i = 0; i < tuple.Item1.IntervalData.Count; i++)
                {
                    var data = tuple.Item1.IntervalData[i];

                    ws.Cells[i + 2, 1].Value = data.Interval;
                    ws.Cells[i + 2, 2].Value = data.Ratio;
                    ws.Cells[i + 2, 3].Value = data.LeftValue;
                    ws.Cells[i + 2, 4].Value = data.RightValue;
                }
            }

            package.SaveAs(new FileInfo(filePath));
        }

        public static void LogMultiDimensional(CountableMultiDimensionalFunc funcData, Dimensions result,
            string filePath)
        {
            LogMultiDimensional(new List<(CountableMultiDimensionalFunc, Dimensions)>
            {
                (funcData, result)
            }, filePath);
        }

        public static void LogMultiDimensional(List<(CountableMultiDimensionalFunc, Dimensions)> funcData,
            string filePath)
        {
            var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("report");

            ws.Cells[1, 1].Value = "Start point";
            ws.Cells[1, 2].Value = "FunctionEpsilon";
            ws.Cells[1, 3].Value = "IterationCount";
            ws.Cells[1, 4].Value = "CallCount";
            ws.Cells[1, 5].Value = "Result";

            for (var i = 0; i < funcData.Count; i++)
            {
                var data = funcData[i].Item1;
                var resPoint = funcData[i].Item2;

                ws.Cells[i + 2, 1].Value = string.Join("; ", data.StartPoint.Coords.Select(d => d.ToString("F4")));
                ws.Cells[i + 2, 2].Value = data.FunctionEpsilon;
                ws.Cells[i + 2, 3].Value = data.IterationCount;
                ws.Cells[i + 2, 4].Value = data.CallCount;
                ws.Cells[i + 2, 5].Value = string.Join("; ", resPoint.Coords.Select(d => d.ToString("F4")));
            }

            package.SaveAs(new FileInfo(filePath));
        }
    }
}
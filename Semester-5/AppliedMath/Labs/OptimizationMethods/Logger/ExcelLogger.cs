using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lab1.Models;
using OfficeOpenXml;

namespace Lab1.Logger
{
    public static class ExcelLogger
    {
        public static void LogInterval(List<LinearLogData> logDataList, string filePath)
        {
            var package = new ExcelPackage();

            var resultSheet = package.Workbook.Worksheets.Add("Results");
            resultSheet.Cells[1, 1].Value = "Method";
            resultSheet.Cells[1, 2].Value = "Result";
            for (var i = 0; i < logDataList.Count; i++)
            {
                resultSheet.Cells[i + 2, 1].Value = logDataList[i].Title;
                resultSheet.Cells[i + 2, 2].Value = logDataList[i].Result;
            }

            foreach (var logData in logDataList)
            {
                var ws = package.Workbook.Worksheets.Add(logData.Title);
                ws.Cells[1, 1].Value = "Interval";
                ws.Cells[1, 2].Value = "Ratio";
                ws.Cells[1, 3].Value = "Left value";
                ws.Cells[1, 4].Value = "Right value";

                for (var i = 0; i < logData.Args.IntervalData.Count; i++)
                {
                    var data = logData.Args.IntervalData[i];

                    ws.Cells[i + 2, 1].Value = data.Interval;
                    ws.Cells[i + 2, 2].Value = data.Ratio;
                    ws.Cells[i + 2, 3].Value = data.LeftValue;
                    ws.Cells[i + 2, 4].Value = data.RightValue;
                }

                var epsSheet = package.Workbook.Worksheets.Add($"{logData.Title} eps");
                epsSheet.Cells[1, 1].Value = "Eps";
                epsSheet.Cells[1, 2].Value = "Call count";
                for (var i = 0; i < logData.EpsilonData.Count; i++)
                {
                    epsSheet.Cells[i + 2, 1].Value = logData.EpsilonData[i].Item1;
                    epsSheet.Cells[i + 2, 2].Value = logData.EpsilonData[i].Item2;
                }
            }

            package.SaveAs(new FileInfo(filePath));
        }
        

        public static void LogMultiDimensional(List<(CountableMultiDimensionalFunc, Dimensions, double)> funcDataList,
            string filePath)
        {
            var package = new ExcelPackage();

            var ws = package.Workbook.Worksheets.Add("report");

            ws.Cells[1, 1].Value = "Start point";
            ws.Cells[1, 2].Value = "FunctionEpsilon";
            ws.Cells[1, 3].Value = "IterationCount";
            ws.Cells[1, 4].Value = "CallCount";
            ws.Cells[1, 5].Value = "Result point";
            ws.Cells[1, 6].Value = "Result value";

            for (var i = 0; i < funcDataList.Count; i++)
            {
                var data = funcDataList[i].Item1;
                var resPoint = funcDataList[i].Item2;
                var resValue = funcDataList[i].Item3;

                ws.Cells[i + 2, 1].Value = string.Join("; ", data.StartPoint.Coords.Select(d => d.ToString("F4")));
                ws.Cells[i + 2, 2].Value = data.FunctionEpsilon;
                ws.Cells[i + 2, 3].Value = data.IterationCount;
                ws.Cells[i + 2, 4].Value = data.CallCount;
                ws.Cells[i + 2, 5].Value = string.Join("; ", resPoint.Coords.Select(d => d.ToString("F4")));
                ws.Cells[i + 2, 6].Value = resValue;

            }

            package.SaveAs(new FileInfo(filePath));
        }
    }
}
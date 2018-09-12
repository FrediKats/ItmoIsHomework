using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lab1.Models;
using OfficeOpenXml;

namespace Lab1.Logger
{
    public static class ExcelLogger
    {
        public static void Log(List<CountableFunc> args, string filePath = "report.xlsx")
        {
            Log(args.Select(a => new[] { a.Epsilon, a.CallCount }).ToArray(), filePath);
        }

        public static void Log(List<CountableMultiDimensionalFunc> args, string filePath = "report.xlsx")
        {
            //TODO: check
            Log(args.Select(a => new[] { a.FunctionEpsilon, a.CallCount }).ToArray(), filePath);
        }

        public static void LogInterval(CountableFunc funcData, string filePath = "report.xlsx")
        {
            Log(funcData.IntervalData.Select(d => d.GetDataArray()).ToArray(), filePath);
        }

        public static void LogMultiDimensional(CountableMultiDimensionalFunc funcData, double[] result, string filePath)
        {
            LogMultiDimensional(new List<(CountableMultiDimensionalFunc, double[])>
            {
                (funcData, result)
            }, filePath);
        }

        public static void LogMultiDimensional(List<(CountableMultiDimensionalFunc, double[])> funcData, string filePath)
        {
            var package = new ExcelPackage();
            ExcelWorksheet ws = package.Workbook.Worksheets.Add("report");

            ws.Cells[1, 1].Value = "Start point";
            ws.Cells[1, 2].Value = "FunctionEpsilon";
            ws.Cells[1, 3].Value = "IterationCount";
            ws.Cells[1, 4].Value = "CallCount";
            ws.Cells[1, 5].Value = "Result";

            for (var i = 0; i < funcData.Count; i++)
            {
                var data = funcData[i].Item1;
                var resPoint = funcData[i].Item2;

                ws.Cells[i + 2, 1].Value = string.Join("; ", data.StartPoint.Select(d => d.ToString("F4")));
                ws.Cells[i + 2, 2].Value = data.FunctionEpsilon;
                ws.Cells[i + 2, 3].Value = data.IterationCount;
                ws.Cells[i + 2, 4].Value = data.CallCount;
                ws.Cells[i + 2, 5].Value = string.Join("; ", resPoint.Select(d => d.ToString("F4")));
            }

            package.SaveAs(new FileInfo(filePath));
        }

        private static void Log<T>(T[][] data, string filePath)
        {
            var package = new ExcelPackage();
            ExcelWorksheet ws = package.Workbook.Worksheets.Add("report");

            for (var i = 0; i < data.Length; i++)
            {
                for (var j = 0; j < data[i].Length; j++)
                {
                    ws.Cells[i + 1, j + 1].Value = data[i][j];

                }
            }

            package.SaveAs(new FileInfo(filePath));
        }
    }
}
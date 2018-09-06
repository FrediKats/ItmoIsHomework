using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;

namespace Lab1.Tools
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

        private static void Log<T>(T[][] data, string filePath)
        {
            var package = new ExcelPackage();
            ExcelWorksheet ws = package.Workbook.Worksheets.Add("report");

            for (var i = 0; i < data.Length; i++)
            {
                for (var j = 0; j < data[i].Length; j++)
                {
                    ws.Cells[i, j].Value = data[i][j];

                }
            }

            package.SaveAs(new FileInfo(filePath));
        }
    }
}
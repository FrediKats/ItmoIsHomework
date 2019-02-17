using System.IO;

namespace AppliedMath.Lab1
{
    public static class DataReader
    {
        public static string[] ReadInput(string filePath)
        {
            return File
                .ReadAllText(filePath)
                .Replace('\n', ' ')
                .Replace("  ", " ")
                .Split(" ");
        }
    }
}
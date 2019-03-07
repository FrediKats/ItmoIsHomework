using System.IO;
using System.Linq;

namespace AppliedMath.Lab2
{
    public class MatrixReader
    {
        public static double[][] ReadMatrix()
        {
            return File
                .ReadAllLines("Input.txt")
                .Select(row => row.Split("\t").Select(v => double.Parse(v) / 100).ToArray())
                .ToArray();
        }
    }
}
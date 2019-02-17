using System;

namespace AppliedMath.Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string[] data = DataReader.ReadInput("InputData.txt");
            Console.WriteLine(data.Length);
        }
    }
}

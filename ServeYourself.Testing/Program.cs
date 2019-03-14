using System;
using ServeYourself.Core;

namespace ServeYourself.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            var serveService = new ServeService();
            for (int i = 0; i < 50; i++)
            {
                serveService.Iteration();
                Console.WriteLine(serveService.GetStatistic());
            }
        }
    }
}

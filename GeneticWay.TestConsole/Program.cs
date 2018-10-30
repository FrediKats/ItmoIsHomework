using GeneticWay.Core.Services;

namespace GeneticWay.TestConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var simulationManager = new SimulationManager();
            while (true)
            {
                simulationManager.MakeIteration(100);
            }
        }
    }
}
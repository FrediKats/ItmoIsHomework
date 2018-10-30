using GeneticWay.Core.Services;

namespace GeneticWay.TestConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SimulationManager simulationManager = new SimulationManager();
            while (true)
            {
                simulationManager.MakeIteration(100);
            }
        }
    }
}
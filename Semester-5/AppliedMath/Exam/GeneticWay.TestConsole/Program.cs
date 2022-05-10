using GeneticWay.Core.Legacy;

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
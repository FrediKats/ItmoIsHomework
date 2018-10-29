namespace GeneticWay.Models
{
    public class Simulation
    {
        public ForceField Field { get; set; }
        public int IterationCount { get; }
        public double DistanceToEnd { get; }

        private bool _isExecuted = false;

        public Simulation(ForceField field)
        {
            Field = field;
        }

        public void Start()
        {
            _isExecuted = true;
            // TODO: implement simulation
        }
    }
}
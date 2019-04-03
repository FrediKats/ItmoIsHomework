namespace AppliedMath.Lab4.Models
{
    public class SystemState
    {
        public int K { get; set; }
        public int M { get; set; }
        public int N { get; set; }
        public int S { get; set; }

        public override string ToString()
        {
            return $"K: {K}\nM: {M}\nN: {N}\nS: {S}";
        }
    }
}
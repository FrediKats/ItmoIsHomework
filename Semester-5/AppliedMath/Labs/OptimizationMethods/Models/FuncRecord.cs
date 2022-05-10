namespace Lab1.Models
{
    public class FuncRecord
    {
        public FuncRecord(double interval, double ratio, double leftValue, double rightValue)
        {
            Interval = interval;
            Ratio = ratio;
            LeftValue = leftValue;
            RightValue = rightValue;
        }

        public double Interval { get; }
        public double Ratio { get; }
        public double LeftValue { get; }
        public double RightValue { get; }

        public double[] GetDataArray()
        {
            return new[] {Interval, Ratio, LeftValue, RightValue};
        }
    }
}
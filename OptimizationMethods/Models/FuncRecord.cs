namespace Lab1.Models
{
    public class FuncRecord
    {
        public FuncRecord(double interval, double div, double leftValue, double rightValue)
        {
            Interval = interval;
            Div = div;
            LeftValue = leftValue;
            RightValue = rightValue;
        }

        public double Interval { get; }
        public double Div { get; }
        public double LeftValue { get; }
        public double RightValue { get; }

        public double[] GetDataArray()
        {
            return new[] {Interval, Div, LeftValue, RightValue};
        }
    }
}
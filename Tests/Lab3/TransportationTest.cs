using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab3;

namespace Tests.Lab3
{
    [TestClass]
    public class TransportationTest
    {
        [TestMethod]
        public void Var1()
        {
            double[] producers = { 50, 30, 10 };
            double[] consumers = { 30, 30, 10, 20 };

            double[][] tariffs =
            {
                new double[] { 1, 2, 4, 1 },
                new double[] { 2, 3, 1, 5 },
                new double[] { 3, 2, 4, 4 },
            };

            TransportationMatrix transportation = new TransportationMatrix(producers, consumers, tariffs);

        }
    }
}

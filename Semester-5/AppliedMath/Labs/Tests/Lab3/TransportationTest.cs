using System.Linq;
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
                new double[] { 3, 2, 4, 4 }
            };
            var transportation = new TransportationMatrix(producers, consumers, tariffs);

            double[][] answers =
            {
                new double[] { 30, 0, 0, 20 },
                new double[] { 0, 20, 10, 0 },
                new double[] { 0, 10, 0, 0 }
            };
            bool isSame = transportation.Plan.Compare(answers);
            Assert.IsTrue(isSame);
        }

        [TestMethod]
        public void Var2()
        {
            double[] producers = { 120, 280, 160 };
            double[] consumers = { 130, 220, 60, 70 };
            double[][] tariffs =
            {
                new double[] { 1, 7, 9, 5 },
                new double[] { 4, 2, 6, 8 },
                new double[] { 3, 8, 1, 2 }
            };
            var transportation = new TransportationMatrix(producers, consumers, tariffs);

            double[][] answers =
            {
                new double[] { 120, 0, 0, 0, 0 },
                new double[] { 0, 220, 0, 0, 60 },
                new double[] { 10, 0, 60, 70, 20 }
            };
            bool isSame = transportation.Plan.Compare(answers);
            Assert.IsTrue(isSame);
        }

        [TestMethod]
        public void Var3()
        {
            double[] producers = { 90, 30, 40};
            double[] consumers = { 70, 30, 20, 40 };
            double[][] tariffs =
            {
                new double[] { 2, 3, 4, 3 },
                new double[] { 5, 3, 1, 2 },
                new double[] { 2, 1, 4, 2 }
            };
            var transportation = new TransportationMatrix(producers, consumers, tariffs);

            double[][] answers =
            {
                new double[] { 70, 0, 0, 20 },
                new double[] { 0, 0, 20, 10 },
                new double[] { 0, 30, 0, 10 }
            };
            bool isSame = transportation.Plan.Compare(answers);
            Assert.IsTrue(isSame);
        }

        [TestMethod]
        public void Var4()
        {
            double[] producers = { 40, 30, 30 };
            double[] consumers = { 30, 70 };
            double[][] tariffs =
            {
                new double[] { 1, 2 },
                new double[] { 3, 2 },
                new double[] { 1, 4 }
            };
            var transportation = new TransportationMatrix(producers, consumers, tariffs);

            double[][] answers =
            {
                new double[] { 0, 40 },
                new double[] { 0, 30 },
                new double[] { 30, 0 }
            };
            bool isSame = transportation.Plan.Compare(answers);
            Assert.IsTrue(isSame);
        }
        [TestMethod]
        public void Var5()
        {
            double[] producers = { 90, 30, 40 };
            double[] consumers = { 50, 60, 10 };
            double[][] tariffs =
            {
                new double[] { 1, 2, 4 },
                new double[] { 1, 3, 4 },
                new double[] { 2, 2, 3 }
            };
            var transportation = new TransportationMatrix(producers, consumers, tariffs);

            double[][] answers =
            {
                new double[] { 50, 40, 0, 0 },
                new double[] { 0, 0, 0, 30 },
                new double[] { 0, 20, 10, 10 }
            };
            bool isSame = transportation.Plan.Compare(answers);
            Assert.IsTrue(isSame);
        }
    }
}

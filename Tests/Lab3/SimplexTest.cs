using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab3;

namespace Tests.Lab3
{
    [TestClass]
    public class SimplexTest
    {
        [TestMethod]
        public void Var1()
        {
            Fraction[][] A = 
            {
                new Fraction[] { 1, 2, -1, 2, 4 },
                new Fraction[] { 0, -1, 2, 1, 3 },
                new Fraction[] { 1, -3, 2, 2, 0 },
            };
            Fraction[] b = { 1, 3, 4 };
            Fraction[] c = { 1, 3, 2, 1, 4 };

            Simplex simplexMatrix = new Simplex(A, b, c);
            Fraction[] answers = new Fraction[] {0, 0, 1, 1, 0};

            Assert.IsTrue(simplexMatrix.Plan.Compare(answers));
        }

        [TestMethod]
        public void Var2()
        {
            Fraction[][] A = 
            {
                new Fraction[] { -1, 3, 0, 2, 1 },
                new Fraction[] { 2, -1, 1, 2, 3 },
                new Fraction[] { 1, -1, 2, 1, 0 },
            };
            Fraction[] b = { 1, 2, 4 };
            Fraction[] c = { 1, -3, 2, 1, 4 };

            Simplex simplexMatrix = new Simplex(A, b, c);
            Fraction[] answers = new Fraction[] {(1, 8), (3, 8), (17, 8), 0, 0};

            Assert.IsTrue(simplexMatrix.Plan.Compare(answers));
        }

        [TestMethod]
        public void Var3()
        {
            Fraction[][] A = 
            {
                new Fraction[] { -1, 3, 0, 2, 1 },
                new Fraction[] { 2, -1, 1, 2, 3 },
                new Fraction[] { 1, -1, 2, 1, 0 },
            };
            Fraction[] b = { 1, 4, 5 };
            Fraction[] c = { -1, 0, -2, 5, 4 };

            Simplex simplexMatrix = new Simplex(A, b, c);
            Fraction[] answers = {(5, 4), (3, 4), (9, 4), 0, 0};

            Assert.IsTrue(simplexMatrix.Plan.Compare(answers));
        }

        [TestMethod]
        public void Var4()
        {
            Fraction[][] A = 
            {
                new Fraction[] { 2, 3, 1, 2, 1 },
                new Fraction[] { 2, 1, -3, 2, 1 },
                new Fraction[] { 2, 1, 2, 1, 0 },
            };
            Fraction[] b = { 1, 3, 1 };
            Fraction[] c = { -1, 1, -2, 1, 5 };
            try
            {
                Simplex simplexMatrix = new Simplex(A, b, c);
            }
            catch (Exception e)
            {
                Assert.IsTrue(true);
                return;
            }
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void Var5()
        {
            Fraction[][] A = 
            {
                new Fraction[] { 2, 1, 3, 4 },
                new Fraction[] { 1, -1, 2, 1 },
                new Fraction[] { 0, 0, 1, 3 },
            };

            Fraction[] b = { 2, 4, 1 };
            Fraction[] c = { -2, 3, 4, 1 };
            try
            {
                Simplex simplexMatrix = new Simplex(A, b, c);
            }
            catch (Exception e)
            {
                Assert.IsTrue(true);
                return;
            }
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void Var9()
        {
            Fraction[][] A = 
            {
                new Fraction[] { 1, 2, 3, 1, 2, 5 },
                new Fraction[] { 2, -3, 1, 2, 1, 4 },
            };

            Fraction[] b = { 1, 2 };
            Fraction[] c = { -2, 3, 4, -1, 2, 1 };


            Fraction[] answers = { 1, 0, 0, 0, 0, 0 };
        }
    }
}

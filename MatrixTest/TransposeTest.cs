using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixTest
{
    [TestClass]
    public class TransposeTest
    {
        [TestMethod]
        public void TestTransitiveAdd()
        {
            // (A+B)^T = A^T + B^T

            var m1 = MatrixHelper.GetRectangularMatrix(3, 2);
            var m2 = MatrixHelper.GetRectangularMatrix(3, 2, -50, 50);

            var addition = m1 + m2;
            var additionTranspose = addition.Transpose();

            var m1Transpose = m1.Transpose();
            var m2Transpose = m2.Transpose();

            var m1Tm2TSum = m1Transpose + m2Transpose;

            Assert.AreEqual(additionTranspose, m1Tm2TSum);
        }

        [TestMethod]
        public void TestTransitiveMultiply()
        {
            // (AB)^T = (B^T)(A^T)
            
            var m1 = MatrixHelper.GetRectangularMatrix(3, 3);
            var m2 = MatrixHelper.GetRectangularMatrix(3, 3, -50, 50);

            // (AB)^T
            var mult = m1 * m2;
            var multTranspose = mult.Transpose();

            // (B^T)(A^T)
            var m1Transpose = m1.Transpose();
            var m2Transpose = m2.Transpose();

            var m2Tm1T = m2Transpose * m1Transpose;

            Assert.AreEqual(multTranspose, m2Tm1T);
        }

        [TestMethod]
        public void TestTransitiveScalar()
        {
            // (cA)^T = c(A^T)

            double _const = 5;

            var m1 = MatrixHelper.GetRectangularMatrix(3, 2);

            var scaled = m1 * _const;
            var scaledTranspose = scaled.Transpose();

            var m1Transpose = m1.Transpose();
            var m1TransposeScaled = m1Transpose * _const;

            Assert.AreEqual(scaledTranspose, m1TransposeScaled);
        }

        [TestMethod]
        public void TestTransitiveInverse()
        {
            // (A^T)^-1 = (A^-1)^T

            var m1 = MatrixHelper.GetSquareMatrix(3);

            var m1Transpose = m1.Transpose();
            var m1TransposeInverse = m1Transpose.Inverse();

            var m1Inverse = m1.Inverse();
            var m1InverseTranspose = m1Inverse.Transpose();

            Assert.AreEqual(m1TransposeInverse, m1InverseTranspose);
        }

        // see DeterminantTest::TestTranspose
        
    }
}

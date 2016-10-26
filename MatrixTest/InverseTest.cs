using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixTest
{
    [TestClass]
    public class InverseTest
    {
        [TestMethod]
        public void TestInverse()
        {
            // A^-1 = (A^-1)^-1
            var matrix = MatrixHelper.GetSquareMatrix(3);

            Assert.AreEqual(true, matrix.Invertible());

            var inverse = matrix.Inverse();
            var inverseInverse = inverse.Inverse();

            Assert.AreEqual(matrix, inverseInverse);

            var inverseInverseInverse = inverseInverse.Inverse();

            Assert.AreEqual(inverse, inverseInverseInverse);
        }

        [TestMethod]
        public void TestInverseScalar()
        {
            // (kA)^-1 = (k^-1)(A^-1)

            double _const = 5;

            var A = MatrixHelper.GetSquareMatrix(3);

            var kA = A * _const;
            var kAInverse = kA.Inverse();

            var kInverse = 1 / _const;
            var AInverse = A.Inverse();

            var kInverseAInverse = AInverse * kInverse;

            Assert.AreEqual(kAInverse, kInverseAInverse);
        }

        [TestMethod]
        public void TestInverseTranspose()
        {
            // (A^T)^-1 = (A^-1)^T

            var A = MatrixHelper.GetSquareMatrix(3);

            var ATranspose = A.Transpose();
            var ATransposeInverse = ATranspose.Inverse();

            var AInverse = A.Inverse();
            var AInverseTranspose = AInverse.Transpose();

            Assert.AreEqual(ATransposeInverse, AInverseTranspose);
        }

        [TestMethod]
        public void TestInvolution()
        {
            // I^T = I

            var I3 = MatrixHelper.GetIdentityMatrix(3);
            var I3Inverse = I3.Inverse();

            Assert.AreEqual(I3, I3Inverse);
        }

        [TestMethod]
        public void TestInverseAdjugate()
        {
            // (A^-1) = ( 1/det(A) )( adj(A) )

            var A = MatrixHelper.GetSquareMatrix(3);

            var AInverse = A.Inverse();

            var ADet = A.Determinant();
            var AAdj = A.Adjoint();

            var AdjDet = AAdj * (1 / ADet);

            Assert.AreEqual(AInverse, AdjDet);
        }
    }
}

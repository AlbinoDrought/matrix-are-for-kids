using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MatrixAreForKids;

namespace MatrixTest
{
    [TestClass]
    public class DeterminantTest
    {
        [TestMethod]
        public void TestTransitiveScalar()
        {
            // det(cA) = det(A) * c^n

            int _n = 3;

            double _const = 5;
            var A = MatrixHelper.GetSquareMatrix(_n);

            var cA = A * _const; 
            var cADet = cA.Determinant(); // det(cA)

            var ADet = A.Determinant();
            var cN = Math.Pow(_const, _n);

            var cNADet = ADet * cN; // det(A) * c^n

            Assert.AreEqual(cADet, cNADet);
            
        }
    }
}

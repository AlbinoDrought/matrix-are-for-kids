using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixTest
{
    [TestClass]
    public class AdjugateTest
    {
        [TestMethod]
        public void TestIdentity()
        {
            // adj(I) = I

            var I3 = MatrixHelper.GetIdentityMatrix(3);
            var I3Adj = I3.Adjoint();

            Assert.AreEqual(I3, I3Adj);
        }

        [TestMethod]
        public void TestTransitiveProduct()
        {
            // adj(AB) = ( adj(B) )( adj(A) )

            var A = MatrixHelper.GetSquareMatrix(3);
            var B = MatrixHelper.GetSquareMatrix(3, -50, 50);

            var AB = A * B;
            var ABAdj = AB.Adjoint();

            var BAdj = B.Adjoint();
            var AAdj = A.Adjoint();

            // BAdjAAdjABABJjajdjJABj
            var BAdjAAdj = BAdj * AAdj;

            Assert.AreEqual(ABAdj, BAdjAAdj);
        }

        [TestMethod]
        public void TestTransitiveScalar()
        {
            // adj(cA) = ( c^(n - 1) )( adj(A) )

            int _n = 3;

            double _const = 5;
            var A = MatrixHelper.GetSquareMatrix(_n);

            var cA = A * _const;
            var cAAdj = cA.Adjoint(); // adj(cA)

            var cNInverse = Math.Pow(_const, _n - 1);
            var AAdj = A.Adjoint();

            var cNAAdj = AAdj * cNInverse; // ( c^(n - 1) )( adj(A) )

            Assert.AreEqual(cAAdj, cNAAdj);
        }
    }
}

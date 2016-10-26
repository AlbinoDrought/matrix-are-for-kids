using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixTest
{
    [TestClass]
    public class AliasTest
    {

#pragma warning disable CS0618 // disable obsolete warning
        [TestMethod]
        public void TestAdjointAliases()
        {
            var A = MatrixHelper.GetSquareMatrix(3);

            var A1 = A.Adjoint();
            var A2 = A.Adjugate();
            var A3 = A.Adjunct();

            Assert.AreEqual(A1, A2);
            Assert.AreEqual(A1, A3);
        }

#pragma warning disable CS0618 // disable obsolete warning
        [TestMethod]
        public void TestDeterminantAliases()
        {
            var A = MatrixHelper.GetSquareMatrix(3);

            var A1 = A.Determinant();
            var A2 = A.Det();

            Assert.AreEqual(A1, A2);
        }
    }
}

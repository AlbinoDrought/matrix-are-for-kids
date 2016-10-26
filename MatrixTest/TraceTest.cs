using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixTest
{
    [TestClass]
    public class TraceTest
    {
        [TestMethod]
        public void TestTransitiveAdd()
        {
            // tr(A + B) = tr(A) + tr(B)

            var A = MatrixHelper.GetRectangularMatrix(3, 2);
            var B = MatrixHelper.GetRectangularMatrix(3, 2, -50, 50);

            var addition = A + B;
            var traceAddition = addition.Trace();

            var traceA = A.Trace();
            var traceB = B.Trace();

            var traceABSum = traceA + traceB;

            Assert.AreEqual(traceAddition, traceABSum);
        }

        [TestMethod]
        public void TestTransitiveScalar()
        {
            // tr(cA) = ctr(A)
            double _const = 5;

            var A = MatrixHelper.GetRectangularMatrix(3, 2);

            var traceA = A.Trace();
            var scaledTraceA = traceA * _const;

            var scaledA = A * _const;
            var traceScaledA = scaledA.Trace();

            Assert.AreEqual(scaledTraceA, traceScaledA);
        }

        [TestMethod]
        public void TestTransitiveTransposeProducts()
        {
            // tr( (X^T)Y ) = tr( X(Y^T) ) = tr( (Y^T)X ) = tr( Y(X^T) )
            var X = MatrixHelper.GetSquareMatrix(3);
            var Y = MatrixHelper.GetSquareMatrix(3, -50, 50);

            var XT = X.Transpose();
            var YT = Y.Transpose();

            var XTY = XT * Y;
            var XYT = X * YT;
            var YTX = YT * X;
            var YXT = Y * XT;

            double trace = XTY.Trace();

            Assert.AreEqual(trace, XYT.Trace());
            Assert.AreEqual(trace, YTX.Trace());
            Assert.AreEqual(trace, YXT.Trace());
        }
    }
}

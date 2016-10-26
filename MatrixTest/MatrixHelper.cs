using MatrixAreForKids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixTest
{
    public static class MatrixHelper
    {
        const int RAND_SEED = 98 + 117 + 109;
        const double MIN_RAND = -100;
        const double MAX_RAND = 100;

        private static Random getRandom()
        {
            return new Random(RAND_SEED);
        }

        public static Matrix GetSquareMatrix(int size, double min = MIN_RAND, double max = MAX_RAND)
        {
            return GetRectangularMatrix(size, size, min, max);
        }

        public static Matrix GetRectangularMatrix(int width, int height, double min = MIN_RAND, double max = MAX_RAND)
        {
            var rand = getRandom();

            double[][] rawMatrix = new double[height][];

            for (int y = 0; y < height; y++)
            {
                rawMatrix[y] = new double[width];

                for (int x = 0; x < width; x++)
                {
                    rawMatrix[y][x] = (rand.NextDouble() * (max - min)) + min;
                }
            }

            return new Matrix(rawMatrix);
        }

        public static Matrix GetIdentityMatrix(int size)
        {
            return Matrix.Identity(size);
        }
    }
}

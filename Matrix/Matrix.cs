using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixAreForKids
{
    public class Matrix
    {
        const int EQUALITY_PRECISION = 4; // precision used to compare doubles

        private double[][] matrix;
        private int height = int.MinValue;
        private int width = int.MinValue;

        public int Height
        {
            get
            {
                if(this.height == int.MinValue)
                {
                    this.height = this.matrix.Length;
                }

                return this.height;
            }
        }

        public int Width
        {
            get
            {
                if(this.width == int.MinValue)
                {
                    if(this.matrix.Length <= 0)
                    {
                        this.width = 0;
                    } else
                    {
                        this.width = this.matrix[0].Length;
                    }
                }

                return this.width;
            }
        }
        
        public Matrix (double[][] matrix)
        {
            this.matrix = matrix;
        }

        public double At(int x, int y)
        {
            return this.matrix[y][x];
        }

        public void Set(int x, int y, double value)
        {
            this.matrix[y][x] = value;
        }

        public string RepresentAsString()
        {
            string s = "";

            int width = this.Width;
            int height = this.Height;

            for(int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    s += this.At(x, y) + " ";
                }

                s += "\r\n";
            }

            return s;
        }

        public override string ToString()
        {
            return this.RepresentAsString();
        }

        public Matrix Clone()
        {
            double[][] cloned = Matrix.CloneJagged(this.matrix);

            return new Matrix(cloned);
        }

        #region Operator Overloads
        // array access (get only)
        public double[] this[int index]
        {
            get
            {
                return (double[])this.matrix[index].Clone(); // return clone to prevent writing
            }

            set
            {
                throw new Exception("Cannot set Matrix values directly");
            }
        }

        // mult with const
        public static Matrix operator *(Matrix m, double c)
        {
            Matrix newMatrix = m.Clone();

            for(int x = 0; x < newMatrix.Width; x++) 
            {
                for(int y = 0; y < newMatrix.Height; y++)
                {
                    newMatrix.Set(x, y, newMatrix.At(x, y) * c);
                }
            }

            return newMatrix;
        }

        // matrix mult
        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if(m1.Width != m2.Height)
            {
                throw new Exception("Cannot multiply matrices where m1 width != m2 height");
            }

            int newHeight = m1.Height;
            int newWidth = m2.Width;

            double[][] newMatrix = new double[newHeight][];
            
            for(int y = 0; y < newHeight; y++)
            {
                if(newMatrix[y] == null)
                {
                    newMatrix[y] = new double[newWidth];
                }

                for(int x = 0; x < newWidth; x++)
                {
                    double newVal = 0;

                    for(int xo = 0; xo < m1.Width; xo++)
                    {
                        newVal += m1[y][xo] * m2[xo][x];
                    }

                    newMatrix[y][x] = newVal;
                }
            }

            return new Matrix(newMatrix);
        }

        // add matrices
        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            if(m1.Height != m2.Height || m1.Width != m2.Width)
            {
                throw new Exception("Matrix dimensions must be equal when adding");
            }

            Matrix clone = m1.Clone();

            for(int x = 0; x < clone.Width; x++)
            {
                for(int y = 0; y < clone.Height; y++)
                {
                    clone.Set(x, y, clone.At(x, y) + m2.At(x, y));
                }
            }

            return clone;
        }

        // subtract matrices (add negative)
        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            Matrix negativeM2 = m2 * -1;

            return m1 + negativeM2;
        }

        public static bool operator ==(Matrix m1, Matrix m2)
        {
            return m1.Equals(m2);
        }

        public static bool operator !=(Matrix m1, Matrix m2)
        {
            return !m1.Equals(m2);
        }

        // equals, up to <EQUALITY_PRECISION> precision
        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Matrix)) return false;

            Matrix other = (Matrix)obj;

            if (this.Width != other.Width || this.Height != other.Height) return false;

            for (int x = 0; x < this.Width; x++)
            {
                for (int y = 0; y < this.Height; y++)
                {
                    double myValue = Math.Round(this.At(x, y), EQUALITY_PRECISION);
                    double theirValue = Math.Round(other.At(x, y), EQUALITY_PRECISION);

                    if (myValue != theirValue) return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return this.matrix.GetHashCode();
        }
        #endregion

        public Matrix ExcludeRow(int row)
        {
            double[][] rawMatrix = Matrix.CloneJagged(this.matrix);

            rawMatrix = rawMatrix.RemoveAt(row);

            return new Matrix(rawMatrix);
        }

        public Matrix ExcludeCol(int col)
        {
            double[][] rawMatrix = Matrix.CloneJagged(this.matrix);

            for(int y = 0; y < rawMatrix.Length; y++)
            {
                rawMatrix[y] = rawMatrix[y].RemoveAt(col);
            }

            return new Matrix(rawMatrix);
        }

        public Matrix ExcludeRowCol(int row, int col)
        {
            Matrix m = this.ExcludeRow(row);
            m = m.ExcludeCol(col);

            return m;
        }

        private double _det(Matrix m)
        {
            int col = 0;

            if(m.Width != m.Height)
            {
                throw new Exception("To calculate determinant, matrix must be rectangular (n x n)");
            }

            if(m.Width == 2)
            {
                return (m.At(0, 0) * m.At(1, 1)) - (m.At(0, 1) * m.At(1, 0));
            }

            double det = 0;

            Matrix trimmed = this.ExcludeCol(col);

            for(int y = 0; y < m.Height; y++)
            {
                Matrix trimmedRow = trimmed.ExcludeRow(y);
                double _const = this.At(col, y);

                // if odd, const should be negative
                // that's just the way it is
                if((col + y) % 2 == 1)
                {
                    _const *= -1;
                }

                det += _const * (trimmedRow.Determinant());
            }

            return det;
        }

        public double Determinant()
        {
            return this._det(this);
        }

        #region Aliases - Determinant
        [Obsolete("Det is deprecated, please use Determinant instead.")]
        public double Det()
        {
            return this.Determinant();
        }
        #endregion

        public double Trace()
        {
            int target = this.Width;
            if(this.Height < target)
            {
                target = this.Height;
            }

            double sum = 0;

            for(int i = 0; i < target; i++)
            {
                sum += this.At(i, i);
            }

            return sum;
        }

        public Matrix Minor()
        {
            Matrix newMatrix = this.Clone();

            for(int x = 0; x < newMatrix.Height; x++)
            {
                for(int y = 0; y < newMatrix.Width; y++)
                {
                    Matrix trimmed = this.ExcludeRowCol(y, x);
                    newMatrix.Set(x, y, trimmed.Determinant());
                }
            }

            return newMatrix;
        }

        public Matrix Cofactor()
        {
            Matrix newMatrix = this.Clone();

            for(int x = 0; x < newMatrix.Width; x++)
            {
                for(int y = 0; y < newMatrix.Height; y++)
                {
                    bool negative = (x + y) % 2 == 1;

                    if (!negative) continue;

                    newMatrix.Set(x, y, newMatrix.At(x, y) * -1);
                }
            }

            return newMatrix;
        }
        
        public Matrix Adjoint()
        {
            Matrix minor = this.Minor();

            minor = minor.Cofactor();

            minor = minor.Transpose();

            return minor;
        }

        #region Aliases - Adjoint
        [Obsolete("Adjunct is deprecated, please use Adjoint instead.")]
        public Matrix Adjunct()
        {
            return this.Adjoint();
        }

        [Obsolete("Adjugate is deprecated, please use Adjoint instead.")]
        public Matrix Adjugate()
        {
            return this.Adjoint();
        }
        #endregion

        public Matrix Inverse()
        {
            double det = this.Determinant();

            if(det == 0)
            {
                throw new Exception("This matrix is not invertible - det != 0");
            }

            Matrix adjointed = this.Adjoint();

            return adjointed * (1 / det);
        }

        public bool Invertible()
        {
            return this.Determinant() != 0;
        }

        public Matrix Transpose()
        {
            int width = this.Width;
            int height = this.Height;

            double[][] newMatrix = new double[width][];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (newMatrix[x] == null)
                    {
                        newMatrix[x] = new double[height];
                    }

                    newMatrix[x][y] = this.At(x, y);
                }
            }

            return new Matrix(newMatrix);
        }
        
        #region Static Generator Methods
        // generate an nxn identity matrix
        public static Matrix Identity(int n)
        {
            double[][] ident = new double[n][];

            for (int y = 0; y < n; y++)
            {
                ident[y] = new double[n];

                ident[y][y] = 1;
            }

            return new Matrix(ident);
        }

        // return raw double[][] jagged-array clone
        public static double[][] CloneJagged(double[][] input)
        {
            return input.Select(s => s.ToArray()).ToArray();
        }
        #endregion
    }
}

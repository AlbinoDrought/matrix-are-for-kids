# Matrix Are For Kids

## Introduction

> To aid learning in an Applied Linear Algebra course, I decided to create a simple matrix class to test proofs. 

> The goal of this class is to simplify basic matrix operations in C#.


## Code Samples

> Matrices are initialized with a jagged-array of doubles:

> `var A = new Matrix(new double[][] { ... });`

> **Currently supported operator overloads:**

> *  Multiplying a matrix by a constant - `A * c`

> *  Multiplying a matrix by a matrix - `A * B`

> *  Adding and subtracting matrices - `A + B`, `A - B`

> *  Comparing matrices - `A == B`, `A != B`

> **Currently supported functions:**

> *  Determinant - `A.Determinant();`

> *  Inverse - `A.Inverse();`

> *  Trace - `A.Trace();`

> *  Transpose - `A.Transpose();`

> *  Finding a minor matrix - `A.Minor();`

> *  Finding a cofactor matrix - `A.Cofactor();`

> *  Finding an adjugate matrix - `A.Adjugate();`

> *  Generating an n x n identity matrix - `Matrix.Identity(n);`


using System;

class ShermanMorrison
{
    static void Main()
    {
        
        double[,] Ainv =
        {
            { -4, -1, 0,0 },
            { -1, -4, 0,0 },
            { 0, 0, -4, -1 },
            {0, 0, -1,-4 },
        };

        double[] u = { 0, 1, 0 ,0 };
        double[] v = { 0, 0,1, 0 };

        double[,] updated = ShermanMorrisonUpdate(Ainv, u, v);

        Console.WriteLine("Updated Inverse Matrix:");
        PrintMatrix(updated);
    }

    static double[,] ShermanMorrisonUpdate(double[,] Ainv, double[] u, double[] v)
    {
        int n = u.Length;

        // z = A^{-1} u
        double[] z = MatrixVectorMultiply(Ainv, u);

        // w = (A^{-1})^T v
        double[] w = MatrixVectorMultiply(Transpose(Ainv), v);

        // λ = v^T z
        double lambda = DotProduct(v, z);

        if (Math.Abs(1 + lambda) < 1e-10)
            throw new Exception("Matrix becomes singular.");

        
        double[,] result = new double[n, n];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                result[i, j] = Ainv[i, j] - (z[i] * w[j]) / (1 + lambda);
            }
        }

        return result;
    }

    static double[] MatrixVectorMultiply(double[,] A, double[] x)
    {
        int n = x.Length;
        double[] result = new double[n];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
                result[i] += A[i, j] * x[j];
        }

        return result;
    }

    static double DotProduct(double[] a, double[] b)
    {
        double sum = 0;
        for (int i = 0; i < a.Length; i++)
            sum += a[i] * b[i];
        return sum;
    }

    static double[,] Transpose(double[,] A)
    {
        int n = A.GetLength(0);
        double[,] T = new double[n, n];

        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                T[j, i] = A[i, j];

        return T;
    }

    static void PrintMatrix(double[,] A)
    {
        int n = A.GetLength(0);
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
                Console.Write($"{A[i, j]:F4} ");
            Console.WriteLine();
        }
    }
}
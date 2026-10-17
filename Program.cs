using System;
using System.Collections;
using System.Collections.Generic;

namespace Medvedev
{
    class Programm
    {
        static void Main(string[] args)
        {
            Multiplication();
        }

        public static void Matrix(out int[,] mat1, out int[,] mat2)
        {
            mat1 = new int[2, 2];

            for (int i = 0; i < mat1.GetLength(0); i++)
            {
                for (int j = 0; j < mat1.GetLength(1); j++)
                {
                    Console.WriteLine($"Ввидите число для строки [{i + 1}, {j + 1}]");

                    mat1[i, j] = int.Parse(Console.ReadLine());
                }
            }

            mat2 = new int[2, 2];

            for (int k = 0; k < mat2.GetLength(0); k++)
            {
                for (int l = 0; l < mat2.GetLength(1); l++)
                {
                    Console.WriteLine($"Ввидите число для строки [{k + 1}, {l + 1}]");

                    mat2[k, l] = int.Parse(Console.ReadLine());
                }
            }
        }

        public static void Multiplication()
        {
            Matrix(out int[,] mat1, out int[,] mat2);

            int[,] result = new int[mat1.GetLength(0), mat2.GetLength(1)];

            for (int i = 0; i < mat1.GetLength(0); i++)
            {
                for (int j = 0; j < mat1.GetLength(1); j++)
                {
                    result[i, j] = 0;

                    for (int k = 0; k < mat2.GetLength(1); k++)
                    {
                        result[i, j] += mat1[i, k] * mat2[k, j];
                    }
                }
            }

            Console.WriteLine("Матрица 1:");
            PrintMatrix(mat1);

            Console.WriteLine("Матрица 2:");
            PrintMatrix(mat2);

            Console.WriteLine("Результат умножения:");
            PrintMatrix(result);
        }

        public static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
    }
}
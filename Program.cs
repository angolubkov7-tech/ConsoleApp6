using System;
using System.Collections;
using System.Collections.Generic;

namespace Medvedev
{
    class Programm
    {
        static void Main(string[] args)
        {
            LinkedList<LinkedList<int>> matrixA = new LinkedList<LinkedList<int>>(); //вместо LinkedList<LinkedList<int>> можно использоваь var
            matrixA.AddLast(new LinkedList<int>(new[] { 1, 2, 3 })); //добавляем в главный поезд LinkedList<LinkedList<int>>, поезд с вагонами 1 2 3
            matrixA.AddLast(new LinkedList<int>(new[] { 4, 5, 6 }));

            LinkedList<LinkedList<int>> matrixB = new LinkedList<LinkedList<int>>();
            matrixB.AddLast(new LinkedList<int>(new[] { 7, 8 }));
            matrixB.AddLast(new LinkedList<int>(new[]  {9, 100}));
            matrixB.AddLast(new LinkedList<int>(new[] { 11, 12 }));

            Console.WriteLine("Матрица А:");
            PrintMatrix(matrixA);

            Console.WriteLine("\n Матрица B:");
            PrintMatrix(matrixB);

            LinkedList<LinkedList<int>> result = MultiMatrix(matrixA, matrixB);
            if (result.Count>0 ||  result != null)
            {
                Console.WriteLine("Результат умножения матрицы равен: ");
                PrintMatrix(result);
            }
           
        }

        public static LinkedList<LinkedList<int>> MultiMatrix(LinkedList<LinkedList<int>> a, LinkedList<LinkedList<int>> b)
        {
            int aRows = a.Count; //количество строк в матрице
            int aCols = a.First.Value.Count; //количество столбов в матрице .Value открывает узел LinkedList<> и считает количество столбов
            int bRows = b.Count;
            int bCols = b.First.Value.Count;

            var result = new LinkedList<LinkedList<int>>();

            for (int i = 0; i < aRows; i++)
            {
                var rowResult = new LinkedList<int>();
                for (int j = 0; j < bCols; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < aCols; k++)
                    {
                        //Встроенный метод  он берет самый первый элемент главного списка .First и по цепочке связей через .Next отсчитывает i строчек вниз
                        //Вторая часть ElementAt(k) находится внутри строчки и идет на k шагов в право
                        int valA = a.ElementAt(i).ElementAt(k);
                        int valB = b.ElementAt(k).ElementAt(j);

                        sum += valA * valB;
                    }

                    rowResult.AddLast(sum);
                }
                result.AddLast(rowResult);
            }
            return result;
        }

        public static void PrintMatrix(LinkedList<LinkedList<int>> matrix)
        {
            foreach (var row  in matrix)
            {
                foreach(var col in row)
                {
                    Console.Write(col + "\t");
                }
                Console.WriteLine();
            }
        }
       
    }
}
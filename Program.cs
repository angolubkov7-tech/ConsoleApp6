using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Medvedev
{
    class Programm
    {
        static void Main(string[] args)
        {
            Days(out int[,] temperatureMatrix);

            
            double[] averages = ArithmeticMean(temperatureMatrix);

            Console.WriteLine("\nСредняя температура по месяцам (до сортировки):");
            for (int i = 0; i < averages.Length; i++)
            {
                Console.WriteLine($"Месяц {i + 1}:\t{averages[i]:F2} °C");
            }

            Array.Sort(averages);

            Console.WriteLine("\nОтсортированные средние температуры (по возрастанию):");
            for (int i = 0; i < averages.Length; i++)
            {
                Console.Write($"{averages[i]:F2} °C\t");
            }
            Console.WriteLine();

            Console.ReadKey();
        }

        public static void Days(out int[,] day)
        {
            day = new int[12, 30];//двумерный массив где столбец(12) - это месяц в котором хранится 30 строк тоесть дней

            Random rnd = new Random();

            for (int i = 0; i < day.GetLength(0); i++) 
            {
                for (int j = 0; j < day.GetLength(1); j++) 
                {
                   
                    day[i, j] = rnd.Next(-20, 31);
                }
            }
        }

       
        public static double[] ArithmeticMean(int[,] day)
        {
            int totalMonths = day.GetLength(0); //12 столбцов - месяцев
            int daysInMonth = day.GetLength(1); 

            
            double[] monthlyAverages = new double[totalMonths];

            for (int i = 0; i < totalMonths; i++) 
            {
                double sum = 0;

                for (int j = 0; j < daysInMonth; j++)
                {
                    sum += day[i, j]; 
                }

                monthlyAverages[i] = sum / daysInMonth;
            }

            return monthlyAverages;
        }
    }
}

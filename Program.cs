using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Medvedev
{
    class Programm
    {
        static void Main(string[] args)
        {
            Dictionary<string, int[]> temperatureData = new Dictionary<string, int[]>();
            string[] monthNames = {"Январь", "Февраль", "Март", "Апрель", "Май", "Июнь",
                "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"};

            Random rnd = new Random();

            for (int i = 0; i < monthNames.Length; i++)
            {
                int[] days = new int[30];
                for (int j = 0; j < 30; j++)
                {
                    if (i == 11 || i == 0 || i == 1)
                    {
                        days[j] = rnd.Next(-25, -4);
                    }
                    else if (i >= 2 && i <= 4)            
                        days[j] = rnd.Next(-5, 16);
                    else if (i >= 5 && i <= 7)             
                        days[j] = rnd.Next(18, 33);
                    else                                   
                        days[j] = rnd.Next(0, 16);
                }

                temperatureData.Add(monthNames[i], days);
            }

            double[] monthlyAverages = Calculator(temperatureData);

            Array.Sort(monthlyAverages);

            foreach (double avg in monthlyAverages)
            {
                Console.WriteLine($"{avg:F2} °C \t");
            }

            Console.WriteLine();
        }

        public static double[] Calculator(Dictionary<string, int[]> weatherDict)
        {
            double[] averages = new double[12];
            int index = 0;

            foreach(var pair in weatherDict)
            {
                int[] daysArry = pair.Value;

                double sum = 0;

                for (int i = 0; i < daysArry.Length; i++)
                {
                    sum += daysArry[i];
                }

                double average = sum/daysArry.Length;
                averages[index] = average;

                index++;

            }
            return averages;
        }

       
    }
}

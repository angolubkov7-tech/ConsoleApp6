using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Medvedev
{
    class Programm
    {
        static void Main(string[] args)
        {
            Console.Write("Ввидите номер задачи: ");
            int i = int.Parse(Console.ReadLine());

            
            List<int> image = new List<int>(64);

            foreach(var emply in image)
            {
                image[emply] = emply+1; 
            }

            Console.WriteLine("До перетасовки");
            foreach (var emplee in image)
            {
                Console.WriteLine(image[emplee]);
            }

            shuffling(image);

            Console.WriteLine("После перетасовки");
            foreach (var emple in image)
            {
                Console.WriteLine(image[emple]);
            }

        }
        
        public static void shuffling(List<int> im)
        {
            Random rnd = new Random();

            for (int i = 0; i < im.Count; i++)
            {
                int j = rnd.Next(i + 1);
                int temp = im[i];
                im[i] = im[j];

                im[j] = temp;
            }
        }


    }
}
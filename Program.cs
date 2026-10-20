using System;
using System.Collections.Generic;
using System.Net.Http;
using System.IO;
using System.Threading.Channels;


namespace Medvedev
{
    class Program
    {
        public static readonly Random rnd = new Random();

        public static void Main(string[] args)
        {
            var image = new List<int >(64);
            for (int i = 1; i <= 32; i++)
            {
                image.Add(i);   
                image.Add(i);   
            }

            Console.WriteLine("Всего элементов: " + image.Count);   

            Console.WriteLine("До перетасовки:");
            PrintList(image);

            Shuffle(image);

            Console.WriteLine("После перетасовки:");
            PrintList(image);
        }

        
        public static void PrintList(List<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.Write(list[i] + " ");
            }
            Console.WriteLine();
        }

        
        public static void Shuffle(List<int> im)
        {
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
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Medvedev
{
    class Program
    {
        static void Main(string[] args)
        {
            string Filepath = @"C:\Users\Roman\Documents\Текст.txt";

            if (!File.Exists(Filepath))
            {
                Console.WriteLine("Ошибка данный файл не найден");
                return;           
            }

            string Content = File.ReadAllText(Filepath);

            char[] text = Content.ToCharArray();

            if (text.Length == 0)
            {
                Console.WriteLine("Файл найден, но внутри него нет текста.");
            }
            else
            {
                foreach (char c in text)
                {
                    
                    Console.Write(c);
                }
                Console.WriteLine();
            }
        }
    }
}
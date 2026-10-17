using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Medvedev
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string Filepath = @"C:\Users\Roman\Documents\text.txt";

            if (!File.Exists(Filepath))
            {
                Console.WriteLine("Ошибка: данный файл не найден");
                return;
            }

            string Content = File.ReadAllText(Filepath);

            if (Content.Length == 0)
            {
                Console.WriteLine("Файл найден, но внутри него нет текста.");
            }
            else
            {
                Console.WriteLine("--- Содержимое файла ---");
                foreach (char c in Content)
                {
                    Console.Write(c);
                }
                Console.WriteLine();
            }

            char[] textArray = Content.ToCharArray();

            LetterCheck(textArray);

        }

        static void LetterCheck(char[] text)
        {
            char[] ruVowels = { 'а', 'е', 'ё', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я' };
            char[] ruConsonants = { 'б', 'в', 'г', 'д', 'ж', 'з', 'й', 'к', 'л', 'м', 'н', 'п', 'р', 'с', 'т', 'ф', 'х', 'ц', 'ч', 'ш', 'щ' };

            char[] enVowels = { 'a', 'e', 'i', 'o', 'u', 'y' };
            char[] enConsonants = { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'z' };

            // Счетчики для русского языка
            int ruVowelsCount = 0;
            int ruConsonantsCount = 0;

            // Счетчики для английского языка
            int enVowelsCount = 0;
            int enConsonantsCount = 0;

            for (int i = 0; i < text.Length; i++)
            {
                // Приводим текущий символ к нижнему регистру 
                char lowerChar = char.ToLower(text[i]);

                // Проверяем русские буквы с помощью метода .Contains() из System.Linq
                if (ruVowels.Contains(lowerChar))
                {
                    ruVowelsCount++;
                }
                else if (ruConsonants.Contains(lowerChar))
                {
                    ruConsonantsCount++;
                }
                // Проверяем английские буквы
                else if (enVowels.Contains(lowerChar))
                {
                    enVowelsCount++;
                }
                else if (enConsonants.Contains(lowerChar))
                {
                    enConsonantsCount++;
                }
            }
            
            Console.WriteLine($"Русские буквы:  Гласных = {ruVowelsCount}, Согласных = {ruConsonantsCount}");
            Console.WriteLine($"Английские буквы: Гласных = {enVowelsCount}, Согласных = {enConsonantsCount}");
        }
    }
}

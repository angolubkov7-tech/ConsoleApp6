using System;
using System.Collections.Generic;
using System.Net.Http;
using System.IO;
using System.Threading.Channels;


namespace Medvedev
{
    struct Student
    {
        public string Surname;
        public string Name;
        public int YearBirth;
        public string Exam;
        public int Points;


        public Student(string Surname, string Name, int YearBirth, string Exam, int Points)
        {
            this.Surname = Surname;
            this.Name = Name;
            this.YearBirth = YearBirth;
            this.Exam = Exam;
            this.Points = Points;
        }

        public string FullName //создаем ключ для Dictionary
        {
            get { return Surname + " " + Name; } //читает что нужно вернуть когда кто то использует FullName
        }

        public override string ToString()
        {
            return FullName + "|" + YearBirth + "год рождения | экзамен" + Exam + "| баллы" + Points;
        }
    }

    class Program
    {
        public static void Main()
        {
            Group group = new Group();


            Console.WriteLine("=== Меню ===");
            Console.WriteLine("  Новый студент");
            Console.WriteLine("  Удалить");
            Console.WriteLine("  Сортировать");
            Console.Write("Введите команду: ");

            string input = Console.ReadLine();

            if (input == "Новый студент")
            {
                group.Add();
            }

            else if (input == "Удалить")
            {
                group.Remove();
            }
            else if (input == "Сортировать")
            {
                group.Sort();
            }
            else { Console.WriteLine("Команда введена не верно"); }
        }
    }

    class Group
    {

        private Dictionary<string, Student> students = new Dictionary<string, Student>();
        private const string FillePatch = "student.txt";

        public void LoadFromFille() //без static т.к нужна было бы ссылка на обьект
        {
            if (!File.Exists(FillePatch))
            {
                Console.WriteLine($"Файл {FillePatch} не найден будем работать с пустым файлом");
                return;
            }

            string[] lines = File.ReadAllLines(FillePatch); //читаем файл и заносим в массив lines
            int ik = 0; //количество студентов

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                string[] parts = line.Split(','); //разделяем студентов из файла

                Student s = new Student(

                    parts[0].Trim(), //Surname
                    parts[1].Trim(),// Name
                    int.Parse(parts[2]),//YearBrith
                    parts[3].Trim(),//Exam
                    int.Parse(parts[4]));//Points

                students[s.FullName] = s; //Берет Фамилию и Имя студента и склеивает их через пробел и возращает строку благодаря get и заносит как копию ключа
                ik++;
            }
            Console.WriteLine($"Закруженно {ik} студентов");
        }

        public void Add()
        {
            Console.Write("Фамилия: ");
            string surname = Console.ReadLine();

            Console.Write("Имя: ");
            string name = Console.ReadLine();

            Console.Write("Год рождения: ");
            int yearBirth = int.Parse(Console.ReadLine());

            Console.Write("Экзамен: ");
            string exam = Console.ReadLine();

            Console.Write("Баллы: ");
            int points = int.Parse(Console.ReadLine());

            Student s = new Student(surname, name, yearBirth, exam, points);

            if (students.ContainsKey(s.FullName))
            {
                Console.WriteLine($"Студент {s.FullName} уже есть!");
                return;
            }

            students[s.FullName] = s;
            Console.WriteLine($"Студент {s.FullName} добавлен");

        }

        public void Remove()
        {
            Console.Write("Фамилия: ");
            string surname = Console.ReadLine();

            Console.Write("Имя: ");
            string name = Console.ReadLine();

            string key = surname + " " + name;

            if (students.Remove(key))
            {
                Console.WriteLine($"Студент {key} удален");
                return;
            }
            else
            {
                Console.WriteLine($"Студента {key} не существует");
            }
        }

        public void Sort()
        {
            List<Student> sotred = new List<Student>(students.Values);
            sotred.Sort(ComparePoints);

            Console.WriteLine("Студенты по возрастанию баллов: ");

            for (int i = 0; i < sotred.Count; i++)
            {
                Console.WriteLine(sotred[i]);
            }
        }

        private static int ComparePoints(Student a, Student b)
        {
            if (a.Points < b.Points) return -1;
            if (a.Points > b.Points) return 1;
            return 0;
        }
    }

}
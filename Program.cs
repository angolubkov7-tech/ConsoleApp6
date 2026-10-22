using System;
using System.Collections.Generic;

namespace Medvedev
{
    class Grandma
    {
        public string Name;
        public int Age;
        public Dictionary<string, string> IllnessMedication; //болезнь лекарство
        public Hospital CurrentHospital; //null если не в больнице, текущая больница

        public Grandma(string name, int age)
        {
            Name = name;
            Age = age;

            IllnessMedication = new Dictionary<string, string>();
            CurrentHospital = null;
        }

        public void AddIllness(string illness, string medication)
        {
            IllnessMedication[illness] = medication;
        }

        public int IllnessCount //количество болезней берутся из количества словаря
        {
            get { return IllnessMedication.Count; }
        }

        public string IllnessesToString()
        {
            if (IllnessCount == 0) return "нет";
            string result = "";
            bool first = true;

            foreach (KeyValuePair<string, string> pair in IllnessMedication)
            {
                if (!first) result += ", ";
                result += pair.Key + "(лекарство: " + pair.Value + ')';
                first = false;
            }
            return result;
        }
    }

    class Hospital
    {
        public string Name;
        public List<string> TreatedIllnesses;
        public int Capacity;
        public List<Grandma> Patient;

        public Hospital(string name, int capcity)
        {
            Name = name;
            Capacity = capcity;

            TreatedIllnesses = new List<string>();
            Patient = new List<Grandma>();
        }

        public void AddTreatedIllness(string illness)
        {
            TreatedIllnesses.Add(illness);
        }

        public bool HasFreeSpace
        {
            get { return Patient.Count < Capacity; }
        }

        public double FillPercent //на сколько процентов больница заполнена 
        {
            get
            {
                if (Capacity == 0) return 0;
                return (double)Patient.Count / Capacity * 100;
            }
        }

        public double TreatPercent(Grandma g) // сколько болезней больница лечит у бабки в процентах
        {
            if (g.IllnessCount == 0) return 100; //пойдет любая бабка

            int k = 0;
            foreach (string illness in g.IllnessMedication.Keys)
            {
                if (TreatedIllnesses.Contains(illness)) k++;
            }
            return (double)k / g.IllnessCount * 100; //сравниваем болезни бабки с болезнми больницы и если совпало то добавляем 1, делим на количество болезней бабки
        }

        public void Admit(Grandma g)
        {
            Patient.Add(g);
            g.CurrentHospital = this;
        }
    }

    class Program
    {
        static Stack<Hospital> hospitals = new Stack<Hospital>();
        static Queue<Grandma> waitingGrandmas = new Queue<Grandma>();
        static List<Grandma> allGrandmas = new List<Grandma>(); //для вывода
        static List<Grandma> streetGrandmas = new List<Grandma>(); //кто остался на улице

        static void Main()
        {
            InitHospitals();

            bool running = true;
            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("=== МЕНЮ ===");
                Console.WriteLine("1. Добавить бабулю (ввод с клавиатуры)");
                Console.WriteLine("2. Распределить очередь по больницам");
                Console.WriteLine("3. Показать всех бабуль и больницы");
                Console.WriteLine("4. Добавить демо-бабуль (для быстрой проверки)");
                Console.WriteLine("0. Выход");
                Console.Write("Выбор: ");

                string input = Console.ReadLine();
                if (input == null) input = "";
                input = input.Trim();

                if (input == "1") AddGrandma();
                else if (input == "2") ProcessQueue();
                else if (input == "3") ShowAll();
                else if (input == "0") running = false;
                else Console.WriteLine("Неверный выбор");
            }
        }

        static void InitHospitals()
        {
            Hospital h1 = new Hospital("Городская", 3);
            h1.AddTreatedIllness("Грипп");
            h1.AddTreatedIllness("Давление");
            h1.AddTreatedIllness("Радикулит");
            hospitals.Push(h1);

            Hospital h2 = new Hospital("Сельская", 2);
            h2.AddTreatedIllness("Простуда");
            h2.AddTreatedIllness("Грипп");
            hospitals.Push(h2);

            Hospital h3 = new Hospital("Областная", 4);
            h3.AddTreatedIllness("Радикулит");
            h3.AddTreatedIllness("Давление");
            h3.AddTreatedIllness("Артрит");
            hospitals.Push(h3);

            Hospital h4 = new Hospital("Санаторий", 1);
            h4.AddTreatedIllness("Артрит");
            h4.AddTreatedIllness("Простуда");
            hospitals.Push(h4);
        }

        static void AddGrandma()
        {
            Console.Write("Имя бабули: ");
            string name = Console.ReadLine();


            Console.Write("Возраст: ");
            int age = int.Parse(Console.ReadLine());

            Grandma g = new Grandma(name, age);

            Console.Write("Сколько болезней у бабули: ");
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                Console.Write($"Болезнь № {i + 1}: ");
                string illness = Console.ReadLine();

                Console.Write($"Какое лекарство пьете от {illness}: ");
                string med = Console.ReadLine();

                g.AddIllness(illness, med);
            }

            waitingGrandmas.Enqueue(g); //в очереди
            allGrandmas.Add(g);

            Console.WriteLine($"Бабуля {name} встала в очредь (в очереди: {waitingGrandmas.Count})");
        }

        static void ProcessQueue()
        {
            if (waitingGrandmas.Count == 0)
            {
                Console.WriteLine("Очередь пуста — некого распределять.");
                return;
            }

            while (waitingGrandmas.Count > 0)
            {
                Grandma g = waitingGrandmas.Dequeue();   // FIFO — по порядку прихода
                Hospital target = FindHospital(g);

                if (target != null)
                {
                    target.Admit(g);
                    Console.WriteLine("Бабуля " + g.Name + " → больница \"" + target.Name + "\"");
                }
                else
                {
                    streetGrandmas.Add(g);
                    if (g.IllnessCount == 0)
                        Console.WriteLine("Бабуля " + g.Name + " не смогла попасть даже спросить — все больницы полны.");
                    else
                        Console.WriteLine("Бабуля " + g.Name + " осталась на улице плакать 😭");
                }
            }


        }

        static Hospital FindHospital(Grandma g)
        {
            foreach (Hospital h in hospitals)   // стек: сверху вниз
            {
                if (!h.HasFreeSpace) continue;

                // Бабуля без болезней — первая свободная
                if (g.IllnessCount == 0) return h;

                // Есть болезни — нужно строго больше 50%
                if (h.TreatPercent(g) > 50) return h;
            }
            return null;   // ни одна не подошла
        }

        static void ShowAll()
        {
            Console.WriteLine();
            Console.WriteLine("========== БАБУЛИ (" + allGrandmas.Count + ") ==========");
            if (allGrandmas.Count == 0) Console.WriteLine("(пока никого)");

            for (int i = 0; i < allGrandmas.Count; i++)
            {
                Grandma g = allGrandmas[i];
                Console.WriteLine((i + 1) + ". " + g.Name + ", " + g.Age + " лет");
                Console.WriteLine("   Болезни: " + g.IllnessesToString());

                if (g.CurrentHospital != null)
                {
                    if (g.IllnessCount == 0)
                        Console.WriteLine("   Статус: больница \"" + g.CurrentHospital.Name + "\" (спрашивает)");
                    else
                        Console.WriteLine("   Статус: больница \"" + g.CurrentHospital.Name
                            + "\" (лечит " + g.CurrentHospital.TreatPercent(g).ToString("F1") + "% её болезней)");
                }
                else if (streetGrandmas.Contains(g))
                {
                    if (g.IllnessCount > 0)
                        Console.WriteLine("   Статус: 😭 на улице плачет");
                    else
                        Console.WriteLine("   Статус: на улице (все больницы полны)");
                }
                else
                {
                    Console.WriteLine("   Статус: в очереди (ждёт)");
                }
            }

            Console.WriteLine();
            Console.WriteLine("========== БОЛЬНИЦЫ (" + hospitals.Count + ") ==========");
            Console.WriteLine("(стек, сверху вниз)");
            int index = 1;
            foreach (Hospital h in hospitals)
            {
                Console.WriteLine(index + ". Больница \"" + h.Name + "\"");
                Console.Write("   Лечит: ");
                if (h.TreatedIllnesses.Count == 0) Console.Write("(ничего)");
                else
                {
                    for (int i = 0; i < h.TreatedIllnesses.Count; i++)
                    {
                        if (i > 0) Console.Write(", ");
                        Console.Write(h.TreatedIllnesses[i]);
                    }
                }
                Console.WriteLine();
                Console.WriteLine("   Заполненность: " + h.Patients.Count + "/" + h.Capacity
                    + " (" + h.FillPercent.ToString("F1") + "%)");

                if (h.Patient.Count > 0)
                {
                    Console.Write("   Пациенты: ");
                    for (int i = 0; i < h.Patient.Count; i++)
                    {
                        if (i > 0) Console.Write(", ");
                        Console.Write(h.Patient[i].Name);
                    }
                    Console.WriteLine();
                }
                index++;
            }

            Console.WriteLine();
            Console.WriteLine("В очереди ждут: " + waitingGrandmas.Count);
            Console.WriteLine("На улице: " + streetGrandmas.Count);


        }
    }
}
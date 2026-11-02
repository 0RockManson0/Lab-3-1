using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

public struct Toy
{
    public string Name;
    public double Price;
    public int AgeMin;
    public int AgeMax;
}

internal static class FileTasks
{
    private static int ChooseMode()
    {
        int mode = 0;
        bool isValid = false;
        
        while (!isValid)
        {
            Console.WriteLine("\nВыберите режим:");
            Console.WriteLine("1 - Случайная генерация");
            Console.WriteLine("2 - Ручной ввод");
            mode = ConsoleHelper.ReadInt("Ваш выбор: ");

            if (mode == 1 || mode == 2)
            {
                isValid = true;
            }
            else
            {
                Console.WriteLine(
                    "Некорректный ввод. " +
                    "Введите 1 или 2.");
            }
        }
        return mode;
    }

    private static void ReadBinaryFile(string path)
    {
        Console.WriteLine(
            $"\nСодержимое файла {path}:");
        
        if (!File.Exists(path))
        {
            Console.WriteLine("Файл не найден.");
            return;
        }
        
        BinaryReader reader = new BinaryReader(
            File.Open(path, FileMode.Open));
        int num = 0;
        
        try
        {
            while (reader.BaseStream.Position < 
                   reader.BaseStream.Length)
            {
                num = reader.ReadInt32();
                Console.Write(num + " ");
            }
        }
        finally
        {
            reader.Close();
        }
        Console.WriteLine();
    }

    private static void ReadTextFile(string path)
    {
        Console.WriteLine(
            $"\nСодержимое файла {path}:");
        
        if (!File.Exists(path))
        {
            Console.WriteLine("Файл не найден.");
            return;
        }
        
        StreamReader reader = new StreamReader(path);
        string line = "";
        
        try
        {
            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }
        finally
        {
            reader.Close();
        }
    }

    private static void FillBinaryRandom(
        string path, int count)
    {
        Random rnd = new Random();
        BinaryWriter writer = new BinaryWriter(
            File.Open(path, FileMode.Create));
        int i = 0;
        
        try
        {
            for (i = 0; i < count; i++)
            {
                writer.Write(rnd.Next(-10, 10));
            }
        }
        finally
        {
            writer.Close();
        }
    }

    private static void FillBinaryManual(
        string path, int count)
    {
        BinaryWriter writer = new BinaryWriter(
            File.Open(path, FileMode.Create));
        int i = 0;
        int num = 0;
        
        try
        {
            for (i = 0; i < count; i++)
            {
                num = ConsoleHelper.ReadInt(
                    $"Число {i + 1}: ");
                writer.Write(num);
            }
        }
        finally
        {
            writer.Close();
        }
    }

    public static void Task4()
    {
        string pathIn = "input4.bin";
        string pathOut = "output4.bin";

        string fullPathIn = Path.GetFullPath(pathIn);
        string fullPathOut = Path.GetFullPath(pathOut);
        
        Console.WriteLine(
            $"\nПолный путь к исходному файлу:");
        Console.WriteLine(fullPathIn);
        Console.WriteLine(
            $"\nПолный путь к результирующему " +
            $"файлу:");
        Console.WriteLine(fullPathOut);

        int mode = ChooseMode();
        int count = ConsoleHelper.ReadInt(
            "Количество чисел: ");

        if (mode == 1)
        {
            FillBinaryRandom(pathIn, count);
            Console.WriteLine(
                "Файл заполнен случайными " +
                "данными.");
        }
        else
        {
            FillBinaryManual(pathIn, count);
            Console.WriteLine(
                "Файл заполнен вручную.");
        }

        ReadBinaryFile(fullPathIn);

        List<int> uniqueNumbers = new List<int>();
        BinaryReader reader = new BinaryReader(
            File.Open(pathIn, FileMode.Open));
        int num = 0;
        bool exists = false;
        int i = 0;
        
        try
        {
            while (reader.BaseStream.Position < 
                   reader.BaseStream.Length)
            {
                num = reader.ReadInt32();
                exists = false;
                
                for (i = 0; i < uniqueNumbers.Count; i++)
                {
                    if (uniqueNumbers[i] == num)
                    {
                        exists = true;
                        break;
                    }
                }
                
                if (!exists)
                {
                    uniqueNumbers.Add(num);
                }
            }
        }
        finally
        {
            reader.Close();
        }

        BinaryWriter writer = new BinaryWriter(
            File.Open(pathOut, FileMode.Create));
        
        try
        {
            for (i = 0; i < uniqueNumbers.Count; i++)
            {
                writer.Write(uniqueNumbers[i]);
            }
        }
        finally
        {
            writer.Close();
        }
        
        Console.WriteLine(
            $"Результат записан в {pathOut}");
        ReadBinaryFile(fullPathOut);
    }

    private static void FillToysRandom(
        string path, int count)
    {
        Random rnd = new Random();
        string[] names = {
            "Мяч", "Кукла", "Конструктор",
            "Машинка", "Мишка", "Кубики",
            "Пирамидка", "Пазл"
        };

        List<Toy> toys = new List<Toy>();
        Toy t;
        int i = 0;
        
        for (i = 0; i < count; i++)
        {
            t = new Toy();
            t.Name = names[rnd.Next(names.Length)];
            t.Price = Math.Round(
                rnd.NextDouble() * 1000, 2);
            t.AgeMin = rnd.Next(0, 6);
            t.AgeMax = t.AgeMin + rnd.Next(1, 8);
            toys.Add(t);
        }

        XmlSerializer serializer = 
            new XmlSerializer(typeof(List<Toy>));
        StreamWriter writer = new StreamWriter(path);
        
        try
        {
            serializer.Serialize(writer, toys);
        }
        finally
        {
            writer.Close();
        }
    }

    private static void FillToysManual(
        string path, int count)
    {
        List<Toy> toys = new List<Toy>();
        Toy t;
        int i = 0;
        
        for (i = 0; i < count; i++)
        {
            Console.WriteLine(
                $"\nИгрушка {i + 1}:");
            t = new Toy();
            t.Name = ConsoleHelper.ReadString(
                "Название: ");
            t.Price = ConsoleHelper.ReadDouble(
                "Цена (руб.): ");
            t.AgeMin = ConsoleHelper.ReadInt(
                "Минимальный возраст: ");
            t.AgeMax = ConsoleHelper.ReadInt(
                "Максимальный возраст: ");
            toys.Add(t);
        }

        XmlSerializer serializer = 
            new XmlSerializer(typeof(List<Toy>));
        StreamWriter writer = new StreamWriter(path);
        
        try
        {
            serializer.Serialize(writer, toys);
        }
        finally
        {
            writer.Close();
        }
    }

    public static void Task5()
    {
        string path = "toys.xml";

        Console.WriteLine(
            $"\nПолный путь к файлу:");
        Console.WriteLine(
            Path.GetFullPath(path));

        int mode = ChooseMode();
        int count = ConsoleHelper.ReadInt(
            "Количество игрушек: ");

        if (mode == 1)
        {
            FillToysRandom(path, count);
            Console.WriteLine(
                "Файл заполнен случайными " +
                "данными.");
        }
        else
        {
            FillToysManual(path, count);
            Console.WriteLine(
                "Файл заполнен вручную.");
        }

        Console.WriteLine(
            $"Файл {path} создан (XML).");

        XmlSerializer serializer = 
            new XmlSerializer(typeof(List<Toy>));
        List<Toy> loadedToys;
        StreamReader reader = new StreamReader(path);
        
        try
        {
            loadedToys = (List<Toy>)
                serializer.Deserialize(reader);
        }
        finally
        {
            reader.Close();
        }

        Console.WriteLine(
            "\nВсе игрушки из файла:");
        
        int i = 0;
        Toy t;
        
        for (i = 0; i < loadedToys.Count; i++)
        {
            t = loadedToys[i];
            Console.WriteLine(
                $"  {i + 1}. {t.Name} - " +
                $"{t.Price} руб. " +
                $"({t.AgeMin}-{t.AgeMax} лет)");
        }

        Toy mostExpensive = new Toy();
        bool found = false;
        double maxPrice = -1;

        for (i = 0; i < loadedToys.Count; i++)
        {
            t = loadedToys[i];
            if (t.AgeMin <= 2 && t.AgeMax >= 3)
            {
                if (!found || t.Price > maxPrice)
                {
                    maxPrice = t.Price;
                    mostExpensive = t;
                    found = true;
                }
            }
        }

        Console.WriteLine();
        if (found)
        {
            Console.WriteLine(
                $"Самая дорогая игрушка " +
                $"для детей 2-3 лет:");
            Console.WriteLine(
                $"  {mostExpensive.Name} - " +
                $"{mostExpensive.Price} руб. " +
                $"({mostExpensive.AgeMin}-" +
                $"{mostExpensive.AgeMax} лет)");
        }
        else
        {
            Console.WriteLine(
                "Подходящих игрушек " +
                "для детей 2-3 лет не найдено.");
        }
    }

    private static void FillText6Random(
        string path, int count)
    {
        Random rnd = new Random();
        StreamWriter writer = new StreamWriter(path);
        int i = 0;
        
        try
        {
            for (i = 0; i < count; i++)
            {
                writer.WriteLine(
                    rnd.Next(-100, 100));
            }
        }
        finally
        {
            writer.Close();
        }
    }

    private static void FillText6Manual(
        string path, int count)
    {
        StreamWriter writer = new StreamWriter(path);
        int i = 0;
        int num = 0;
        
        try
        {
            for (i = 0; i < count; i++)
            {
                num = ConsoleHelper.ReadInt(
                    $"Число {i + 1}: ");
                writer.WriteLine(num);
            }
        }
        finally
        {
            writer.Close();
        }
    }

    public static void Task6()
    {
        string path = "numbers6.txt";

        Console.WriteLine(
            $"\nПолный путь к файлу:");
        Console.WriteLine(
            Path.GetFullPath(path));

        int mode = ChooseMode();
        int count = ConsoleHelper.ReadInt(
            "Количество чисел: ");

        if (mode == 1)
        {
            FillText6Random(path, count);
            Console.WriteLine(
                "Файл заполнен случайными " +
                "данными.");
        }
        else
        {
            FillText6Manual(path, count);
            Console.WriteLine(
                "Файл заполнен вручную.");
        }

        ReadTextFile(path);

        int digit = ConsoleHelper.ReadInt(
            "Введите цифру (0-9): ");
        int sum = 0;

        StreamReader reader = new StreamReader(path);
        string line = "";
        int num = 0;
        
        try
        {
            while ((line = reader.ReadLine()) != null)
            {
                if (int.TryParse(line, out num))
                {
                    if (Math.Abs(num) % 10 == digit)
                    {
                        sum += num;
                    }
                }
            }
        }
        finally
        {
            reader.Close();
        }
        
        Console.WriteLine(
            $"Сумма элементов, оканчивающихся " +
            $"на {digit}: {sum}");
    }

    private static void FillText7Random(
        string path, int rows, int cols)
    {
        Random rnd = new Random();
        StreamWriter writer = new StreamWriter(path);
        int i = 0;
        int j = 0;
        string line = "";
        
        try
        {
            for (i = 0; i < rows; i++)
            {
                line = "";
                for (j = 0; j < cols; j++)
                {
                    line += rnd.Next(-50, 50);
                    if (j < cols - 1)
                    {
                        line += " ";
                    }
                }
                writer.WriteLine(line);
            }
        }
        finally
        {
            writer.Close();
        }
    }

    private static void FillText7Manual(
        string path, int rows, int cols)
    {
        StreamWriter writer = new StreamWriter(path);
        int i = 0;
        int j = 0;
        int num = 0;
        string line = "";
        
        try
        {
            for (i = 0; i < rows; i++)
            {
                line = "";
                Console.WriteLine(
                    $"\nСтрока {i + 1}:");
                for (j = 0; j < cols; j++)
                {
                    num = ConsoleHelper.ReadInt(
                        $"Число {j + 1}: ");
                    line += num;
                    if (j < cols - 1)
                    {
                        line += " ";
                    }
                }
                writer.WriteLine(line);
            }
        }
        finally
        {
            writer.Close();
        }
    }

    public static void Task7()
    {
        string path = "numbers7.txt";

        Console.WriteLine(
            $"\nПолный путь к файлу:");
        Console.WriteLine(
            Path.GetFullPath(path));

        int mode = ChooseMode();
        int rows = ConsoleHelper.ReadInt(
            "Количество строк: ");
        int cols = ConsoleHelper.ReadInt(
            "Чисел в строке: ");

        if (mode == 1)
        {
            FillText7Random(path, rows, cols);
            Console.WriteLine(
                "Файл заполнен случайными " +
                "данными.");
        }
        else
        {
            FillText7Manual(path, rows, cols);
            Console.WriteLine(
                "Файл заполнен вручную.");
        }

        ReadTextFile(path);

        int firstElement = 0;
        int minElement = 0;
        bool isFirst = true;

        StreamReader reader = new StreamReader(path);
        string line = "";
        string[] parts;
        int i = 0;
        int num = 0;
        
        try
        {
            while ((line = reader.ReadLine()) != null)
            {
                parts = line.Split(' ');
                for (i = 0; i < parts.Length; i++)
                {
                    if (int.TryParse(parts[i], out num))
                    {
                        if (isFirst)
                        {
                            firstElement = num;
                            minElement = num;
                            isFirst = false;
                        }
                        else if (num < minElement)
                        {
                            minElement = num;
                        }
                    }
                }
            }
        }
        finally
        {
            reader.Close();
        }
        
        Console.WriteLine(
            $"Первый элемент: {firstElement}");
        Console.WriteLine(
            $"Минимальный элемент: {minElement}");
        Console.WriteLine(
            $"Разность: {firstElement - minElement}");
    }

    private static void FillText8Random(string path)
    {
        StreamWriter writer = new StreamWriter(path);
        
        try
        {
            writer.WriteLine("Привет мир");
            writer.WriteLine("Как дела?");
            writer.WriteLine("Это тест без знаков");
            writer.WriteLine(
                "Тут есть запятая, и точка.");
            writer.WriteLine(
                "Hello world without punctuation");
            writer.WriteLine(
                "Another line with no marks");
        }
        finally
        {
            writer.Close();
        }
    }

    private static void FillText8Manual(
        string path, int count)
    {
        StreamWriter writer = new StreamWriter(path);
        int i = 0;
        string line = "";
        
        try
        {
            for (i = 0; i < count; i++)
            {
                line = ConsoleHelper.ReadString(
                    $"Строка {i + 1}: ");
                writer.WriteLine(line);
            }
        }
        finally
        {
            writer.Close();
        }
    }

    public static void Task8()
    {
        string pathIn = "text8.txt";
        string pathOut = "text8_out.txt";
        string punctuation = ".,!?;:-—«»()[]{}";

        Console.WriteLine(
            $"\nПолный путь к исходному файлу:");
        Console.WriteLine(
            Path.GetFullPath(pathIn));
        Console.WriteLine(
            $"\nПолный путь к результирующему " +
            $"файлу:");
        Console.WriteLine(
            Path.GetFullPath(pathOut));

        int mode = ChooseMode();

        if (mode == 1)
        {
            FillText8Random(pathIn);
            Console.WriteLine(
                "Файл заполнен случайными " +
                "данными.");
        }
        else
        {
            int count = ConsoleHelper.ReadInt(
                "Количество строк: ");
            FillText8Manual(pathIn, count);
            Console.WriteLine(
                "Файл заполнен вручную.");
        }

        ReadTextFile(pathIn);

        StreamReader reader = new StreamReader(pathIn);
        StreamWriter writer = new StreamWriter(pathOut);
        string line = "";
        bool hasPunctuation = false;
        int i = 0;
        
        try
        {
            while ((line = reader.ReadLine()) != null)
            {
                hasPunctuation = false;
                for (i = 0; i < line.Length; i++)
                {
                    if (punctuation.IndexOf(line[i]) >= 0)
                    {
                        hasPunctuation = true;
                        break;
                    }
                }
                if (!hasPunctuation)
                {
                    writer.WriteLine(line);
                }
            }
        }
        finally
        {
            reader.Close();
            writer.Close();
        }
        
        Console.WriteLine(
            $"\nСтроки без знаков препинания " +
            $"записаны в {pathOut}");
        ReadTextFile(pathOut);
    }
}
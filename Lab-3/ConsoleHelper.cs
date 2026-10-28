using System;

internal static class ConsoleHelper
{
    private static bool IsEmpty(string s)
    {
        if (s == null)
        {
            return true;
        }
        for (int i = 0; i < s.Length; i++)
        {
            if (!char.IsWhiteSpace(s[i]))
            {
                return false;
            }
        }
        return true;
    }

    public static int ReadInt(string prompt)
    {
        string input = "";
        int result = 0;
        while (true)
        {
            Console.Write(prompt);
            input = Console.ReadLine();
            if (int.TryParse(input, out result))
            {
                return result;
            }
            Console.WriteLine(
                "Некорректный ввод. " +
                "Введите целое число.");
        }
    }

    public static double ReadDouble(string prompt)
    {
        string input = "";
        double result = 0.0;
        while (true)
        {
            Console.Write(prompt);
            input = Console.ReadLine();
            if (double.TryParse(input, out result))
            {
                return result;
            }
            Console.WriteLine(
                "Некорректный ввод. " +
                "Введите вещественное число.");
        }
    }

    public static string ReadString(string prompt)
    {
        string result = "";
        while (true)
        {
            Console.Write(prompt);
            result = Console.ReadLine();
            if (!IsEmpty(result))
            {
                return result;
            }
            Console.WriteLine(
                "Строка не может быть пустой.");
        }
    }
}
using System;

internal class Program
{
    private static void Main(string[] args)
    {
        int choice = 0;
        while (true)
        {
            Console.WriteLine(
                "\nЛабораторная работа №3");
            Console.WriteLine(
                "Выберите задание (1-8) " +
                "или 0 для выхода:");
            choice = ConsoleHelper.ReadInt(
                "Ваш выбор: ");

            switch (choice)
            {
                case 0:
                    Console.WriteLine(
                        "Выход из программы.");
                    return;
                case 1: RunTask1(); break;
                case 2: RunTask2(); break;
                case 3: RunTask3(); break;
                case 4: FileTasks.Task4(); break;
                case 5: FileTasks.Task5(); break;
                case 6: FileTasks.Task6(); break;
                case 7: FileTasks.Task7(); break;
                case 8: FileTasks.Task8(); break;
                default:
                    Console.WriteLine(
                        "Неверный номер задания.");
                    break;
            }
        }
    }

    private static void RunTask1()
    {
        Console.WriteLine(
            "\nЗадание 1, Вариант 13");
        int n = ConsoleHelper.ReadInt(
            "Введите n (строк): ");
        int m = ConsoleHelper.ReadInt(
            "Введите m (столбцов): ");

        Console.WriteLine(
            "\nПервый массив " +
            "(ввод по диагоналям)");
        MatrixClass matrix1 =
            new MatrixClass(n, m);
        Console.WriteLine("\nПервый массив:");
        Console.WriteLine(matrix1);

        Console.WriteLine(
            "\nВторой массив " +
            "(убывающие строки)");
        MatrixClass matrix2 =
            new MatrixClass(n);
        Console.WriteLine("\nВторой массив:");
        Console.WriteLine(matrix2);

        Console.WriteLine(
            "\nТретий массив " +
            "(спираль)");
        MatrixClass matrix3 =
            new MatrixClass(n, "spiral");
        Console.WriteLine("\nТретий массив:");
        Console.WriteLine(matrix3);
    }

    private static void RunTask2()
    {
        Console.WriteLine(
            "\nЗадание 2, Вариант 13");
        int n = ConsoleHelper.ReadInt(
            "Количество депутатов (n): ");
        Console.WriteLine(
            "Вводите 1 для 'Да', 0 для 'Нет'");
        MatrixClass voting =
            new MatrixClass(n, 2, true);
        voting.AnalyzeVoting();
    }

    private static void RunTask3()
    {
        Console.WriteLine(
            "\nЗадание 3, Вариант 13: " +
            "A^T + B - 3*C");
        int n = ConsoleHelper.ReadInt(
            "Размер матриц (n x n): ");

        Console.WriteLine("\nВвод матрицы A:");
        MatrixClass A =
            new MatrixClass(n, n, true);
        Console.WriteLine("\nВвод матрицы B:");
        MatrixClass B =
            new MatrixClass(n, n, true);
        Console.WriteLine("\nВвод матрицы C:");
        MatrixClass C =
            new MatrixClass(n, n, true);

        MatrixClass result = ~A + B - 3 * C;

        Console.WriteLine(
            "\nРезультат A^T + B - 3*C:");
        Console.WriteLine(result);
    }
}
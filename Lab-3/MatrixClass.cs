using System;

internal class MatrixClass
{

    private int[,] _data;
    private int _rows = 0;
    private int _cols = 0;


    public MatrixClass(int n, int m)
    {
        _rows = n;
        _cols = m;
        _data = new int[n, m];

        for (int diff = n - 1;
             diff >= -(m - 1); diff--)
        {
            int startI = Math.Max(0, diff);
            int endI = Math.Min(
                n - 1, m - 1 + diff);
            for (int i = startI;
                 i <= endI; i++)
            {
                int j = i - diff;
                Console.Write($"[{i},{j}]: ");
                _data[i, j] =
                    ConsoleHelper.ReadInt("");
            }
        }
    }
    
    public MatrixClass(int n)
    {
        _rows = n;
        _cols = n;
        _data = new int[n, n];
        Random rnd = new Random();

        for (int i = 0; i < n; i++)
        {
            int[] row = new int[n];
            for (int j = 0; j < n; j++)
            {
                row[j] = rnd.Next(-100, 100);
            }
            for (int a = 0; a < n - 1; a++)
            {
                for (int b = 0;
                     b < n - 1 - a; b++)
                {
                    if (row[b] < row[b + 1])
                    {
                        int temp = row[b];
                        row[b] = row[b + 1];
                        row[b + 1] = temp;
                    }
                }
            }
            for (int j = 0; j < n; j++)
            {
                _data[i, j] = row[j];
            }
        }
    }


    public MatrixClass(int n, string mode)
    {
        _rows = n;
        _cols = n;
        _data = new int[n, n];

        int top = 0;
        int bottom = n - 1;
        int left = 0;
        int right = n - 1;
        int num = 1;

        while (top <= bottom && left <= right)
        {
            for (int i = left; i <= right; i++)
            {
                _data[top, i] = num++;
            }
            top++;
            for (int i = top; i <= bottom; i++)
            {
                _data[i, right] = num++;
            }
            right--;
            if (top <= bottom)
            {
                for (int i = right; i >= left; i--)
                {
                    _data[bottom, i] = num++;
                }
                bottom--;
            }
            if (left <= right)
            {
                for (int i = bottom; i >= top; i--)
                {
                    _data[i, left] = num++;
                }
                left++;
            }
        }
    }
    
    public MatrixClass(int rows, int cols,
                       bool manual)
    {
        _rows = rows;
        _cols = cols;
        _data = new int[rows, cols];

        if (manual)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"[{i},{j}]: ");
                    _data[i, j] =
                        ConsoleHelper.ReadInt("");
                }
            }
        }
    }
    
    public int Rows
    {
        get { return _rows; }
    }

    public int Cols
    {
        get { return _cols; }
    }
    
    public void AnalyzeVoting()
    {
        int sameCount = 0;
        int changedCount = 0;

        for (int i = 0; i < _rows; i++)
        {
            int v1 = _data[i, 0];
            int v2 = _data[i, 1];

            string vote1 = (v1 == 1)
                ? "Да" : "Нет";
            string vote2 = (v2 == 1)
                ? "Да" : "Нет";

            Console.WriteLine(
                $"Депутат {i + 1}: " +
                $"{vote1}, {vote2}");

            if (v1 == v2)
            {
                sameCount++;
            }
            else
            {
                changedCount++;
            }
        }

        Console.WriteLine(
            $"\nПроголосовали одинаково: " +
            $"{sameCount}");
        Console.WriteLine(
            $"Изменили решение: " +
            $"{changedCount}");

        if (sameCount > changedCount)
        {
            Console.WriteLine(
                "Депутатов, проголосовавших " +
                "одинаково, больше.");
        }
        else if (changedCount > sameCount)
        {
            Console.WriteLine(
                "Депутатов, изменивших " +
                "решение, больше.");
        }
        else
        {
            Console.WriteLine(
                "Количество депутатов равно.");
        }
    }
    
    public static MatrixClass operator ~(
        MatrixClass m)
    {
        MatrixClass result = new MatrixClass(
            m._cols, m._rows, false);
        result._rows = m._cols;
        result._cols = m._rows;
        result._data = new int[
            m._cols, m._rows];

        for (int i = 0; i < m._rows; i++)
        {
            for (int j = 0; j < m._cols; j++)
            {
                result._data[j, i] =
                    m._data[i, j];
            }
        }
        return result;
    }

    public static MatrixClass operator +(
        MatrixClass a, MatrixClass b)
    {
        MatrixClass result = new MatrixClass(
            a._rows, a._cols, false);
        for (int i = 0; i < a._rows; i++)
        {
            for (int j = 0; j < a._cols; j++)
            {
                result._data[i, j] =
                    a._data[i, j] +
                    b._data[i, j];
            }
        }
        return result;
    }

    public static MatrixClass operator -(
        MatrixClass a, MatrixClass b)
    {
        MatrixClass result = new MatrixClass(
            a._rows, a._cols, false);
        for (int i = 0; i < a._rows; i++)
        {
            for (int j = 0; j < a._cols; j++)
            {
                result._data[i, j] =
                    a._data[i, j] -
                    b._data[i, j];
            }
        }
        return result;
    }

    public static MatrixClass operator *(
        MatrixClass m, int scalar)
    {
        MatrixClass result = new MatrixClass(
            m._rows, m._cols, false);
        for (int i = 0; i < m._rows; i++)
        {
            for (int j = 0; j < m._cols; j++)
            {
                result._data[i, j] =
                    m._data[i, j] * scalar;
            }
        }
        return result;
    }

    public static MatrixClass operator *(
        int scalar, MatrixClass m)
    {
        return m * scalar;
    }

    public override string ToString()
    {
        string result = "";
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _cols; j++)
            {
                result += _data[i, j]
                    .ToString().PadLeft(6);
            }
            result += "\n";
        }
        return result;
    }
}
using System;

internal class MatrixClass
{

    private int[,] _data;

    public MatrixClass(int n, int m)
    {
        _data = new int[n, m];
        
        int diff = 0;
        int startI = 0;
        int endI = 0;
        int i = 0;
        int j = 0;

        for (diff = n - 1; diff >= -(m - 1); diff--)
        {
            startI = Math.Max(0, diff);
            endI = Math.Min(n - 1, m - 1 + diff);
            
            for (i = startI; i <= endI; i++)
            {
                j = i - diff;
                Console.Write($"[{i},{j}]: ");
                _data[i, j] = ConsoleHelper.ReadInt("");
            }
        }
    }


    public MatrixClass(int n)
    {
        _data = new int[n, n];
        Random rnd = new Random();
        
        int i = 0;
        int j = 0;
        int a = 0;
        int b = 0;
        int temp = 0;
        int[] row;

        for (i = 0; i < n; i++)
        {
            row = new int[n];
            
            for (j = 0; j < n; j++)
            {
                row[j] = rnd.Next(-100, 100);
            }
            
            for (a = 0; a < n - 1; a++)
            {
                for (b = 0; b < n - 1 - a; b++)
                {
                    if (row[b] < row[b + 1])
                    {
                        temp = row[b];
                        row[b] = row[b + 1];
                        row[b + 1] = temp;
                    }
                }
            }
            
            for (j = 0; j < n; j++)
            {
                _data[i, j] = row[j];
            }
        }
    }


    public MatrixClass(int n, string mode)
    {
        _data = new int[n, n];

        int top = 0;
        int bottom = n - 1;
        int left = 0;
        int right = n - 1;
        int num = 1;
        int i = 0;

        while (top <= bottom && left <= right)
        {
            for (i = top; i <= bottom; i++)
            {
                _data[i, left] = num++;
            }
            left++;

            for (i = left; i <= right; i++)
            {
                _data[bottom, i] = num++;
            }
            bottom--;

            if (left <= right)
            {
                for (i = bottom; i >= top; i--)
                {
                    _data[i, right] = num++;
                }
                right--;
            }

            if (top <= bottom)
            {
                for (i = right; i >= left; i--)
                {
                    _data[top, i] = num++;
                }
                top++;
            }
        }
    }


    public MatrixClass(int rows, int cols, bool manual)
    {
        _data = new int[rows, cols];
        
        int i = 0;
        int j = 0;

        if (manual)
        {
            for (i = 0; i < rows; i++)
            {
                for (j = 0; j < cols; j++)
                {
                    Console.Write($"[{i},{j}]: ");
                    _data[i, j] = ConsoleHelper.ReadInt("");
                }
            }
        }
    }


    public int Rows
    {
        get { return _data.GetLength(0); }
    }

    public int Cols
    {
        get { return _data.GetLength(1); }
    }
    
    public void AnalyzeVoting()
    {
        int sameCount = 0;
        int changedCount = 0;
        int i = 0;
        int v1 = 0;
        int v2 = 0;
        string vote1 = "";
        string vote2 = "";

        for (i = 0; i < _data.GetLength(0); i++)
        {
            v1 = _data[i, 0];
            v2 = _data[i, 1];

            vote1 = (v1 == 1) ? "Да" : "Нет";
            vote2 = (v2 == 1) ? "Да" : "Нет";

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
    public static MatrixClass operator ~(MatrixClass m)
    {
        MatrixClass result = new MatrixClass(
            m._data.GetLength(1), 
            m._data.GetLength(0), 
            false);
        
        int i = 0;
        int j = 0;

        for (i = 0; i < m._data.GetLength(0); i++)
        {
            for (j = 0; j < m._data.GetLength(1); j++)
            {
                result._data[j, i] = m._data[i, j];
            }
        }
        return result;
    }

    public static MatrixClass operator +(
        MatrixClass a, MatrixClass b)
    {
        MatrixClass result = new MatrixClass(
            a._data.GetLength(0), 
            a._data.GetLength(1), 
            false);
        
        int i = 0;
        int j = 0;
        
        for (i = 0; i < a._data.GetLength(0); i++)
        {
            for (j = 0; j < a._data.GetLength(1); j++)
            {
                result._data[i, j] = 
                    a._data[i, j] + b._data[i, j];
            }
        }
        return result;
    }

    public static MatrixClass operator -(
        MatrixClass a, MatrixClass b)
    {
        MatrixClass result = new MatrixClass(
            a._data.GetLength(0), 
            a._data.GetLength(1), 
            false);
        
        int i = 0;
        int j = 0;
        
        for (i = 0; i < a._data.GetLength(0); i++)
        {
            for (j = 0; j < a._data.GetLength(1); j++)
            {
                result._data[i, j] = 
                    a._data[i, j] - b._data[i, j];
            }
        }
        return result;
    }

    public static MatrixClass operator *(
        MatrixClass m, int scalar)
    {
        MatrixClass result = new MatrixClass(
            m._data.GetLength(0), 
            m._data.GetLength(1), 
            false);
        
        int i = 0;
        int j = 0;
        
        for (i = 0; i < m._data.GetLength(0); i++)
        {
            for (j = 0; j < m._data.GetLength(1); j++)
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
        int i = 0;
        int j = 0;
        
        for (i = 0; i < _data.GetLength(0); i++)
        {
            for (j = 0; j < _data.GetLength(1); j++)
            {
                result += _data[i, j]
                    .ToString().PadLeft(6);
            }
            result += "\n";
        }
        return result;
    }
}
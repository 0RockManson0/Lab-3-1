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
        _rows = n;
        _cols = n;
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
        _rows = n;
        _cols = n;
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
        _rows = rows;
        _cols = cols;
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
        int i = 0;
        int v1 = 0;
        int v2 = 0;
        string vote1 = "";
        string vote2 = "";

        for (i = 0; i < _rows; i++)
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
            m._cols, m._rows, false);
        result._rows = m._cols;
        result._cols = m._rows;
        result._data = new int[m._cols, m._rows];
        
        int i = 0;
        int j = 0;

        for (i = 0; i < m._rows; i++)
        {
            for (j = 0; j < m._cols; j++)
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
            a._rows, a._cols, false);
        
        int i = 0;
        int j = 0;
        
        for (i = 0; i < a._rows; i++)
        {
            for (j = 0; j < a._cols; j++)
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
            a._rows, a._cols, false);
        
        int i = 0;
        int j = 0;
        
        for (i = 0; i < a._rows; i++)
        {
            for (j = 0; j < a._cols; j++)
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
            m._rows, m._cols, false);
        
        int i = 0;
        int j = 0;
        
        for (i = 0; i < m._rows; i++)
        {
            for (j = 0; j < m._cols; j++)
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
        
        for (i = 0; i < _rows; i++)
        {
            for (j = 0; j < _cols; j++)
            {
                result += _data[i, j]
                    .ToString().PadLeft(6);
            }
            result += "\n";
        }
        return result;
    }
}
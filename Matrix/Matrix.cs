namespace Matrix;
internal class Matrix
{
    private int _rowsCount;
    private int _columnsCount;
    private double[,] _matrix;

    public Matrix(int rows, int columns)
    {
        _rowsCount = rows;
        _columnsCount = columns;
        _matrix = new double[rows, columns];
    }
        
    public Matrix(double[,] matrix)
    {
        _matrix = (double[,])matrix.Clone();
        _rowsCount = matrix.GetLength(0);
        _columnsCount = matrix.GetLength(1);
    }

    public void SetValuesByRow(int row, List<double> rowElements)
    {
        for (int j = 0; j < _columnsCount; j++)
        {
            _matrix[row, j] = rowElements[j];
        }
    }
    public int CountElements(bool isPositive)
    {
        var count = 0;
        var multiplier = isPositive ? 1 : -1;
        foreach(var element in _matrix)
        {
            if (element * multiplier > 0)
            {
                count++;
            }
        }    
        return count;
    }

    public double[,] InsertionSortByRows(bool directOrder)
    {
        var sortedMatrix = (double[,]) _matrix.Clone();
          
        for (var i = 0; i < _rowsCount; i++)
        {
            for (var j = 1; j < _columnsCount; j++)
            {
                var currentElement = sortedMatrix[i, j];
                var k = j - 1;

                while (k >= 0 && (directOrder ? sortedMatrix[i, k] > currentElement : sortedMatrix[i, k] < currentElement))
                {
                    sortedMatrix[i, k + 1] = sortedMatrix[i, k];
                    k--;
                }

                sortedMatrix[i, k + 1] = currentElement;
            }
        }
        return sortedMatrix;
    }

    public double[,] Inverse()
    {
        if (_rowsCount != _columnsCount)
            throw new ArgumentException("Матрица должна быть квадратной!");
        var n = _rowsCount;
        double[,] augmentedMatrix = new double[n, 2 * _columnsCount];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                augmentedMatrix[i, j] = _matrix[i, j];
                augmentedMatrix[i, j + n] = (i == j) ? 1 : 0;
            }
        }

        for (int i = 0; i < n; i++)
        {
            double pivot = augmentedMatrix[i, i];

            if (pivot == 0)
                throw new ArithmeticException("Матрица вырожденная! К ней нет стандартной обратной.");

            for (int j = 0; j < 2 * n; j++)
            {
                augmentedMatrix[i, j] /= pivot;
            }

            for (int k = 0; k < n; k++)
            {
                if (k != i)
                {
                    double factor = augmentedMatrix[k, i];

                    for (int j = 0; j < 2 * n; j++)
                    {
                        augmentedMatrix[k, j] -= factor * augmentedMatrix[i, j];
                    }
                }
            }
        }

        double[,] inverseMatrix = new double[n, n];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                inverseMatrix[i, j] = augmentedMatrix[i, j + n];
            }
        }

        return inverseMatrix;
    }

    public void Print()
    {
        for (int i = 0; i < _rowsCount; i++)
        {
            for (int j = 0; j < _columnsCount; j++)
            {
                Console.Write(_matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}


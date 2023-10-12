namespace Matrix;
class Program
{
    public static void Main()
    {
        Console.Write("Введите количество строк: ");
        var rows = ReadCorrectInt();
        Console.Write("Введите количество столбцов: ");
        var columns = ReadCorrectInt();
        var matrix = new Matrix(rows, columns);

        var parsedElements = new List<double>();
        Console.WriteLine("Вводите элементы матрицы построчно через пробел:");
        for (int i = 0; i < rows; i++)
        {
            var isParseFailed = false;
            parsedElements.Clear();

            string[] elements = Console.ReadLine().Split(' ');

            if (elements.Length != columns)
            {
                Console.WriteLine("Неверное количество элементов в строке! Введи еще раз:");
                i--;
                continue;
            }

            for (int j = 0; j < columns; j++)
            {
                if(double.TryParse(elements[j], out double value))
                {
                    parsedElements.Add(value);
                }
                else
                {
                    Console.WriteLine("Не удается преобразовать в int! Введи строку еще раз:");
                    i--;
                    isParseFailed = true;
                    break;
                }
            }
            if (isParseFailed)
                continue;

            matrix.SetValuesByRow(i, parsedElements);
        }

        // 1
        Console.WriteLine("Количество положительных элементов матрицы: " + matrix.CountElements(true));
        Console.WriteLine("Количество отрицалельных элементов матрицы: " + matrix.CountElements(false));
        Console.WriteLine();

        // 2
        var sortedInDirectOrder = new Matrix(matrix.InsertionSortByRows(true));
        Console.WriteLine("Отсортированная в прямом порядке матрица:");
        sortedInDirectOrder.Print();

        var sortedInReversedOrder = new Matrix(matrix.InsertionSortByRows(false));
        Console.WriteLine("Отсортированная в обратном порядке матрица:");
        sortedInReversedOrder.Print();

        // 3
        Console.WriteLine("Иныертированная матрица:");
        try
        {
            var inversed = new Matrix(matrix.Inverse());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    private static int ReadCorrectInt()
    {
        var result = 0;
        var input = Console.ReadLine();
        while (!int.TryParse(input, out result))
        {
            Console.WriteLine("Некорректный ввод! Попробуйте еще раз.");
            input = Console.ReadLine();
        }
        return result;
    }
    
}
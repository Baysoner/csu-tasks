using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите целочисленное число: ");
        string input = Console.ReadLine();

        if (IsValidInteger(input))
        {
            int sum = CalculateDigitSum(input);
            Console.WriteLine(sum);
        }
        else
        {
            Console.WriteLine("Введено некорректное число.");
        }
    }

    static bool IsValidInteger(string input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            if (i == 0 && (input[i] == '-' || input[i] == '+'))
            {
                continue;
            }

            if (!char.IsDigit(input[i]))
            {
                return false;
            }
        }
        return true;
    }
    static int CalculateDigitSum(string input)
    {
        int sum = 0;
        int startIndex = input[0] == '-' || input[0] == '+' ? 1 : 0;

        for (int i = startIndex; i < input.Length; i++)
        {
            int digit = input[i] - '0'; // преобразование по ascii
            sum += digit;
        }
        
        return sum;
    }
}
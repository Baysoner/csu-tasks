public static class Program
{
    public static void Main()
    {
        int prevInt = 0;
        float prevFloat = 0f;

        while (true)
        {
            string str = Console.ReadLine();

            if (str == "q" || str == "Q")
                break;//выход из программы

            else if (int.TryParse(str, out int intNumber))
            {
                Console.WriteLine((char)intNumber);
            }
            else if (float.TryParse(str, out float floatNumber))
                if (floatNumber == prevFloat)
                    break;
                else
                {
                    prevFloat = floatNumber;
                }
            else
            {
                Console.WriteLine("Не число!!!");
                continue;
            }
        }
    }
}
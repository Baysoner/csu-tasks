using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Введите математическое выражение: ");
        string input = Console.ReadLine().Replace(" ", "");

        var rpn = ToRpn(input);

        Console.WriteLine($"Выражение в ОПЗ: {string.Join(" ", rpn)}");

        Console.WriteLine($"Решени мат. выражения: {CalculateRPN(rpn)}");
    }

    // Преобразование мат. выражения в ОПЗ
    public static List<object> ToRpn(string input)
    {
        Dictionary<object, int> priority = new Dictionary<object, int>
        {
            {'+', 0}, {'-', 0}, {'*', 1}, {'/', 1}, {'(', -1}, {')', 2},
        };
        List<object> rpn = new List<object>();
        Stack<object> stack = new Stack<object>();
        string nums = "";
        for (int i = 0; i < input.Length; i++)
        {
            if (priority.ContainsKey(input[i]))
            {
                if (!string.IsNullOrEmpty(nums))
                {
                    rpn.Add(nums);
                    nums = "";
                }

                if (input[i] == ')')
                {
                    while ((char)stack.Peek() != '(')
                    {
                        rpn.Add(stack.Pop());
                    }
                    stack.Pop();
                }
                else if (stack.Count == 0 || input[i] == '(' || priority[input[i]] > priority[stack.Peek()])
                {
                    stack.Push(input[i]);
                }
                else if (priority[input[i]] <= priority[stack.Peek()])
                {
                    while (stack.Count > 0 && (char)stack.Peek() != '(')
                    {
                        rpn.Add(stack.Pop());
                    }
                    stack.Push(input[i]);
                }
            }
            else
            {
                nums += input[i];
            }
        }
        if (!string.IsNullOrEmpty(nums))
        {
            rpn.Add(nums);
        }
        while (stack.Count > 0)
        {
            rpn.Add(stack.Pop());
        }
        return rpn;
    }

    // Вычисление значения по ОПЗ
    public static double CalculateRPN(List<object> rpn)
    {
        Stack<double> stack = new Stack<double>();
        for (int i = 0; i < rpn.Count; i++)
        {
            if (rpn[i] is string)
            {
                double num = Convert.ToDouble(rpn[i]);
                stack.Push(num);
            }
            else
            {
                var rightOperand = stack.Pop();
                var leftOperand = stack.Pop();
                stack.Push(UseOperator((char)rpn[i], leftOperand, rightOperand));
            }
        }
        return stack.Pop();
    }

    // Метод для вычисления операции
    public static double UseOperator(char @operator, double leftOperand, double rightOperand)
    {
        switch (@operator)
        {
            case '+':
                return leftOperand + rightOperand;
            case '-':
                return leftOperand - rightOperand;
            case '*':
                return leftOperand * rightOperand;
            case '/':
                return leftOperand / rightOperand;
            default: return 0;
        }
    }
}


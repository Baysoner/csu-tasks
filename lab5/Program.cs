using System;
using System.Collections.Generic;

class Token
{
    public  int Priority { get; }
}

class Number : Token
{
    private double value;
    public Number(double value)
    {
        this.value = value;
    }
    public  int Priority => 0;
    public double GetValue() => value;
}
class Operation : Token
{
    private char symbol;
    private int priority;
    public Operation(char symbol, int priority)
    {
        this.symbol = symbol;
        this.priority = priority;
    }
    public int Priority => priority;
    public char GetSymbol() => symbol;
}

class Parenthesis : Token
{
    private char symbol;
    public Parenthesis(char symbol)
    {
        this.symbol = symbol;
    }
    public  int Priority => symbol == '(' ? -1 : 2;
    public char GetSymbol() => symbol;
}

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Введите математическое выражение: ");
        string input = Console.ReadLine().Replace(" ", "");

        var rpn = ToRpn(input);

        Console.WriteLine($"Решение мат. выражения: {CalculateRPN(rpn)}");
    }

    public static List<Token> ToRpn(string input)
    {
        Dictionary<char, int> priority = new Dictionary<char, int>
        {
            {'+', 1}, {'-', 1}, {'*', 2}, {'/', 2}, {'(', 0}, {')', 0},
        };
        List<Token> rpn = new List<Token>();
        Stack<Token> stack = new Stack<Token>();
        string nums = "";
        foreach (char c in input)
        {
            if (char.IsDigit(c) || c == '.')
            {
                nums += c;
            }
            else
            {
                if (!string.IsNullOrEmpty(nums))
                {
                    rpn.Add(new Number(double.Parse(nums)));
                    nums = "";
                }

                if (c == '(')
                {
                    stack.Push(new Parenthesis(c));
                }
                else if (c == ')')
                {
                    while (!(stack.Peek() is Parenthesis) || ((Parenthesis)stack.Peek()).GetSymbol() != '(')
                    {
                        rpn.Add(stack.Pop());
                    }
                    if (stack.Peek() is Parenthesis && ((Parenthesis)stack.Peek()).GetSymbol() == '(')
                    {
                        stack.Pop();
                    }
                }
                else if (priority.ContainsKey(c))
                {
                    while (stack.Count > 0 && stack.Peek() is Operation && priority[((Operation)stack.Peek()).GetSymbol()] >= priority[c])
                    {
                        rpn.Add(stack.Pop());
                    }
                    stack.Push(new Operation(c, priority[c]));
                }
            }
        }

        if (!string.IsNullOrEmpty(nums))
        {
            rpn.Add(new Number(double.Parse(nums)));
        }

        while (stack.Count > 0)
        {
            rpn.Add(stack.Pop());
        }

        return rpn;
    }

    public static double CalculateRPN(List<Token> rpn)
    {
        Stack<double> stack = new Stack<double>();
        foreach (var token in rpn)
        {
            if (token is Number num)
            {
                stack.Push(num.GetValue());
            }
            else if (token is Operation op)
            {
                var rightOperand = stack.Pop();
                var leftOperand = stack.Pop();
                stack.Push(UseOperator(op.GetSymbol(), leftOperand, rightOperand));
            }
        }
        return stack.Pop();
    }

    public static double UseOperator(char @operator, double leftOperand, double rightOperand)
    {
        return @operator switch
        {
            '+' => leftOperand + rightOperand,
            '-' => leftOperand - rightOperand,
            '*' => leftOperand * rightOperand,
            '/' => leftOperand / rightOperand,
        };
    }
}

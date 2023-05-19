using System.Drawing;
using System.Runtime.CompilerServices;

namespace ConsoleApp1
{
    internal class Program
    {
        public interface ILogger
        {
            void Event(string message);
            void Error(string message);
        }
        public class Logger: ILogger
        {
            public void Event(string message)
            {
                ConsoleColor lastColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(message);
                Console.ForegroundColor = lastColor;
            }
            public void Error(string message)
            {
                ConsoleColor lastColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message);
                Console.ForegroundColor = lastColor;
            }
        }
    public interface ICalculator
        {
            public float Sum(in float a, in float b);
        }
        public class Calculator: ICalculator
        {
            Logger logger;
            public Calculator()
            {
                logger = new();
            }
            public float Sum(in float a, in float b)
            {
                logger.Event("Calculator Sum event");
                return a + b;
            }
        }
        public static float InputFloat( string prompt)
        {
            Console.Write(prompt);
            string? resultS;
            float resultF;
            while ((resultS = Console.ReadLine()) is null || !float.TryParse(resultS, out resultF) )
                    Console.Write(prompt);
            return resultF;
        }
        
        static void Main(string[] args)
        {
            Logger logger = new();
            try
            {
                Calculator calc = new();
                if (args.Length >= 2 && float.TryParse(args[0], out float a) && float.TryParse(args[1], out float b))
                    Console.WriteLine($"{args[0]} + {args[1]} = {calc.Sum(a, b)}");
                else
                {
                    a = InputFloat("Enter first number: ");
                    b = InputFloat("Enter second number: ");
                    Console.WriteLine($"{a} + {b} = {calc.Sum(a, b)}");
                }
            }
            catch(Exception e)
            {
                logger.Error(e.Message);
            }
        }
    }
}
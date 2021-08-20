using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA003
{
    class Program
    {
        //Последовательный и параллельный вызов асинхронных операций

        static void Factorial(int n)
        {
            int result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            Console.WriteLine($"Факториал числа {n} равен {result}");
        }
        // определение асинхронного метода
        static async void FactorialAsync()
        {
            await Task.Run(() => Factorial(4));
            await Task.Run(() => Factorial(3));
            await Task.Run(() => Factorial(5));
        }

        static async void FactorialWhenAllAsync()
        {
            Task t1 = Task.Run(() => Factorial(9));
            Task t2 = Task.Run(() => Factorial(8));
            Task t3 = Task.Run(() => Factorial(7));
            await Task.WhenAll(new[] { t1, t2, t3 });
        }

        static void Main(string[] args)
        {
            FactorialAsync();

            FactorialWhenAllAsync();
            Console.Read();
        }
    }
}
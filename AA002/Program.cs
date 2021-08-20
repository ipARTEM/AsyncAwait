using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA002
{
    class Program
    {
        static void Factorial(int n)
        {
            int result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            Console.Write($"Факториал равен {result} ");
        }

        static int FactorialInt(int n)
        {
            int result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }

        // определение асинхронного метода
        static async void FactorialVoidAsync(int n)
        {
            await Task.Run(() => Factorial(n));
            Console.WriteLine(" - FactorialVoidAsync");
        }

        static async Task FactorialTaskAsync(int n)  
        {
            await Task.Run(() => Factorial(n));
            Console.WriteLine(" - FactorialTaskAsync");
        }

        static async Task<int> FactorialTaskTAsync(int n)    //Task - класс
        {
            return await Task.Run(() => FactorialInt(n));
            Console.WriteLine(" - FactorialTaskTAsync");
        }

        static async ValueTask<int> FactorialValueTaskAsync(int n)        //ValueTask - структура
        {
            return await Task.Run(() => FactorialInt(n));
        }

        static async Task Main(string[] args)
        {
            //Возвращение результата из асинхронного метода

            Console.WriteLine("Введите значение для расчета факториала: ");

            int f =Convert.ToInt32( Console.ReadLine());

            FactorialVoidAsync(f);

            FactorialTaskAsync(f);

            int fTaskT = await FactorialTaskTAsync(f);
            Console.WriteLine(fTaskT);

            int fValueTask = await FactorialValueTaskAsync(f);
            Console.WriteLine(fValueTask);

        }
    }
}

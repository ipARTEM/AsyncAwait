using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AA004
{
    class Program
    {
        static void Factorial(int n)
        {
            if (n < 1)
                throw new Exception($"{n} : число не должно быть меньше 1");
            int result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            Console.WriteLine($"Факториал числа {n} равен {result}");
        }

        static async void FactorialAsync(int n)
        {
            try
            {
                await Task.Run(() => Factorial(n));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static async void FactorialAsync2(int n)
        {
            Task task = null;
            try
            {
                task = Task.Run(() => Factorial(n));
                await task;
            }
            catch (Exception ex)
            {
                Console.WriteLine(task.Exception.InnerException.Message);
                Console.WriteLine($"FactorialAsync2 - IsFaulted: {task.IsFaulted}");
            }
        }

        static async Task DoMultipleAsync()
        {
            Task allTasks = null;

            try
            {
                Task t1 = Task.Run(() => Factorial(-3));
                Task t2 = Task.Run(() => Factorial(-5));
                Task t3 = Task.Run(() => Factorial(-10));

                allTasks = Task.WhenAll(t1, t2, t3);
                await allTasks;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DoMultipleAsync - Исключение: " + ex.Message);
                Console.WriteLine("DoMultipleAsync - IsFaulted: " + allTasks.IsFaulted);
                foreach (var inx in allTasks.Exception.InnerExceptions)
                {
                    Console.WriteLine("DoMultipleAsync - Внутреннее исключение: " + inx.Message);
                }
            }
        }

        static async void FactorialFinallyAsync(int n)
        {
            try
            {
                await Task.Run(() => Factorial(n)); ;
            }
            catch (Exception ex)
            {
                await Task.Run(() => Console.WriteLine(ex.Message));
            }
            finally
            {
                await Task.Run(() => Console.WriteLine("FactorialFinallyAsync - await в блоке finally"));
            }
        }

        static void FactorialIsCancellationRequested(int n, CancellationToken token)
        {
            int result = 1;
            for (int i = 1; i <= n; i++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Операция прервана токеном");
                    return;
                }
                result *= i;
                Console.WriteLine($"Факториал числа {i} равен {result}");
                Thread.Sleep(1000);
            }
        }
        // определение асинхронного метода
        static async void FactorialIsCancellationRequestedAsync(int n, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return;
            await Task.Run(() => FactorialIsCancellationRequested(n, token));
        }




        static void Main(string[] args)
        {
            FactorialAsync(-4);
            FactorialAsync(6);

            FactorialAsync2(-4);
            FactorialAsync2(6);

            DoMultipleAsync();

            FactorialFinallyAsync(-5);

            Console.WriteLine("*******************");

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            FactorialIsCancellationRequestedAsync(6, token);
            Thread.Sleep(3000);
            cts.Cancel();
            Console.Read();



            Console.Read();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwaitMiniTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($" -1- Main starting, thread: {Thread.CurrentThread.ManagedThreadId}\n");  //1

            Task<int> number = MyAsync();

            Console.WriteLine($" -5- Main after MyAsync() running, thread: {Thread.CurrentThread.ManagedThreadId}\n");      //5

            Thread.Sleep(3000);

            Console.WriteLine($" -6- Main GetTask isCanceled? {number.IsCanceled}, thread: {Thread.CurrentThread.ManagedThreadId}\n"); //6
            Console.WriteLine($" -9-  Main GetTask result: {number.Result}, thread: {Thread.CurrentThread.ManagedThreadId}\n");    //9

            Console.WriteLine(" -10- Main Finish");                                                                           //10 

            Console.ReadKey();


        }                                                                                                              

        static async Task<int> MyAsync()
        {
            Console.WriteLine($" -2- MyAsync starting, thread: {Thread.CurrentThread.ManagedThreadId}\n");          //2

            int number = await GetTask(1000);
            Console.WriteLine($" -8- MyAsync ending, thread: {Thread.CurrentThread.ManagedThreadId}\n");           //8

            return number;                                                                                                    
        }

        static Task<int> GetTask(int time)                                                                                  
        {
            Console.WriteLine($" -3- GetTask starting, thread: {Thread.CurrentThread.ManagedThreadId}\n");           //3

            return Task.Run(() =>
            {
                for (int i = 0; i < 2; i++)
                {
                    Console.WriteLine($" -4- -7- GetTask cycle, thread: {Thread.CurrentThread.ManagedThreadId}\n");       //4    //7
                    Thread.Sleep(time);

                }
                return 1;
            });

        }
    }
}

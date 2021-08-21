using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AA005
{
    //Асинхронные стримы
    //Asynchronous Streams

    //public interface IAsyncEnumerable<out T>
    //{
    //    IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default);
    //}

    public interface IAsyncEnumerator<out T> : IAsyncDisposable
    {
        T Current { get; }
        ValueTask<bool> MoveNextAsync();
    }
    public interface IAsyncDisposable
    {
        ValueTask DisposeAsync();
    }


    public class Product
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }

    class Program
    {
        public static async IAsyncEnumerable<Product> GetProducts()
        {
            List<Product> products = new List<Product>()
            {
                new Product{Name="Car", Description="A car (or automobile) is a wheeled motor vehicle used for transportation."},
                new Product{Name="Bicycle", Description="A bicycle, also called a bike or cycle, is a human-powered or motor-powered, pedal-driven, single-track vehicle, having two wheels attached to a frame, one behind the other."},
                new Product{Name="Airplane", Description="An airplane or aeroplane (informally plane) is a fixed-wing aircraft that is propelled forward by thrust from a jet engine, propeller, or rocket engine."},
                new Product{Name="Ship", Description="A ship is a large watercraft that travels the world's oceans and other sufficiently deep waterways, carrying goods or passengers, or in support of specialized missions, such as defense, research, and fishing."},
                new Product{Name="Rocket", Description="A rocket  is a projectile that spacecraft, aircraft or other vehicles use to obtain thrust from a rocket engine. "},

            };

            foreach (Product product in products)
            {
                await Task.Delay(1000);
                yield return product;
            }

        }


        static async Task Main(string[] args)
        {
            await foreach (var product in GetProducts())
            {
                Console.WriteLine($"Name: {product.Name}, Description: {product.Description} ");
            }

            await foreach (var number in GetNumbersAsync())
            {
                Console.WriteLine(number);
            }
        }

        public static async IAsyncEnumerable<int> GetNumbersAsync()
        {
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(100);
                yield return i;
            }
        }
    }

    class Repository
    {
        string[] data = { "Tom", "Sam", "Kate", "Alice", "Bob" };
        public async IAsyncEnumerable<string> GetDataAsync()
        {
            for (int i = 0; i < data.Length; i++)
            {
                Console.WriteLine($"Получаем {i + 1} элемент");
                await Task.Delay(500);
                yield return data[i];
            }
        }
    }
}
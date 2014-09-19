using System;
using Microsoft.Owin.Hosting;

namespace Hyperfriendly.WebApi.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            const string baseAddress = "http://localhost:1337/";

            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine("Server running on {0}", baseAddress);
                Console.ReadLine(); 
            }
        }
    }
}

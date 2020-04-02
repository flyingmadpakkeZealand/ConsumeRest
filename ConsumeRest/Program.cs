using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsumeRest
{
    class Program
    {
        static void Main(string[] args)
        {
            string loadString = "Loading REST API";
            Console.Write(loadString);
            Task ping = Worker.Ping();
            while (!ping.IsCompleted)
            {
                Console.SetCursorPosition(loadString.Length,0);
                for (int i = 0; i < 3; i++)
                {
                    Console.Write(".");
                    Thread.Sleep(300);
                }
                Console.SetCursorPosition(loadString.Length, 0);
                Console.Write("   ");
            }
            Console.Clear();
            Console.WriteLine("REST API loaded...");

            Worker.Run();

            for (int i = 0; i < 500; i++)
            {
                Console.WriteLine("Hello");
            }

            Console.WriteLine("All Done!");
            Console.ReadLine();
        }
    }
}

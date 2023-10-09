using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstConsoleAppExample
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World");
            //Console.WriteLine("Hello World");
            //Console.ReadLine();



            int testCharacter = Int32.TryParse(Console.ReadLine());

            Console.WriteLine(testCharacter);
        }
    }
}

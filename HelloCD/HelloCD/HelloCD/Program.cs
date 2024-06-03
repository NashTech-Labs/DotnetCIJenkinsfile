using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloCD
{
    class Program
    {
        private static int majorVersion = 2;
        private static int minorVersion = 0;
        static void Main(string[] args)
        {
            string versionNumber = majorVersion.ToString() + "." + minorVersion.ToString();
            string message = "Hello CD! This program is version number";
            Console.WriteLine(message + " " + versionNumber);
			Console.WriteLine("Its the future!");
            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }
    }
}

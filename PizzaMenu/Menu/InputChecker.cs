using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMenu.Menu
{
    internal class InputChecker
    {
        //for making sure selected number is between range
        public static int GetIntegerInRange(int nMin, int nMax, string nMessage)
        {
            if (nMin > nMax)
            {
                throw new Exception($"Minimum value {nMin} cannot be greater than maximum value {nMax}");
            }

            int result;

            do
            {
                Console.WriteLine(nMessage);
                Console.WriteLine($"Please enter a number between {nMin} and {nMax} inclusive");

                string userInput = Console.ReadLine();

                try
                {
                    result = int.Parse(userInput);
                }
                catch
                {
                    //if input is not a number
                    Console.WriteLine($"{userInput} is not a number");
                    continue;
                }

                if (result >= nMin && result <= nMax)
                {
                    return result;
                }
                //if number is not between ranges
                Console.WriteLine($"{result} is not between {nMin} and {nMax} inclusive");
            } while (true);
        }
    }
}

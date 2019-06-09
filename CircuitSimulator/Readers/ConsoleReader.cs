using CircuitSimulator.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator
{
    public class ConsoleReader
    {
        public int ReadInput()
        {
            // Read console input value and check whether value is a number
            string input = "";
            int number = 0;

            while (String.IsNullOrEmpty(input))
            {
                input = Console.ReadLine();

                if (Int32.TryParse(input, out number))
                {
                    // If input is a negative number, turn it into a positive one
                    if (number < 0)
                        number = number * -1;
                    break;
                }
                else
                {
                    ConsoleWriterSingleton.Instance.ShowUnReadableInput();
                    input = "";
                }
            }

            return number;
        }

        public bool ReadAnswer()
        {
            // Check if user answered whether to calculate another calculation
            string input = "";
            bool answer = false;

            while (String.IsNullOrEmpty(input))
            {
                input = Console.ReadLine().ToUpper();

                if (input.Equals("Y"))
                {
                    answer = true;
                    break;
                }
                else if(input.Equals("N"))
                {
                    answer = false;
                    break;
                }
                else
                {
                    ConsoleWriterSingleton.Instance.ShowUnReadableInput();
                    input = "";
                }
            }
            return answer;
        }

        public void ReadLine()
        {
            Console.ReadLine();
        }
    }
}

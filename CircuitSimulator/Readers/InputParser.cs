using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CircuitSimulator.Input;

namespace CircuitSimulator.Readers
{
    public class InputParser
    {
        public InputValue[] Parse(int number)
        {
            List<InputValue> inputValues = new List<InputValue>();
            foreach (char character in Convert.ToString(number, 2))
            {
                int inputValue;
                Int32.TryParse(character.ToString(), out inputValue);
                inputValues.Add((InputValue)inputValue);
            }

            return inputValues.ToArray();
        }

        public List<InputValue[]> ParseInputs(int[] inputs)
        {
            List<InputValue[]> circuitInputs = new List<InputValue[]>();
            List<InputValue> consoleInputs;
            foreach (int input in inputs)
            {
                consoleInputs = new List<InputValue>();

                foreach (char character in Convert.ToString(input, 2))
                {
                    int inputValue;
                    if (Int32.TryParse(character.ToString(), out inputValue))
                    {
                        InputValue circuitInputValue = (InputValue)inputValue;
                        consoleInputs.Add(circuitInputValue);
                    }
                }

                circuitInputs.Add(consoleInputs.ToArray());
            }

            return circuitInputs;
        }
    }
}

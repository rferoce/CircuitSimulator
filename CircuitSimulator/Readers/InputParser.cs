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
        private List<InputValue> _consoleInputs;
        private List<InputValue[]> _circuitInputs;

        public List<InputValue[]> ParseInputs(int[] inputs)
        {
            _circuitInputs = new List<InputValue[]>();
            
            // Parse int to inputValue
            foreach (int input in inputs)
            {
                ParseIntToInputValue(input);
            }

            return _circuitInputs;
        }

        private void ParseIntToInputValue(int input)
        {
            _consoleInputs = new List<InputValue>();

            foreach (char character in Convert.ToString(input, 2))
            {
                // Parse character to inputValue
                ParseCharToInputValue(character);
            }

            _circuitInputs.Add(_consoleInputs.ToArray());
        }

        private void ParseCharToInputValue(char character)
        {
            int inputValue;
            if (Int32.TryParse(character.ToString(), out inputValue))
            {
                InputValue circuitInputValue = (InputValue)inputValue;
                _consoleInputs.Add(circuitInputValue);
            }
        }
    }
}

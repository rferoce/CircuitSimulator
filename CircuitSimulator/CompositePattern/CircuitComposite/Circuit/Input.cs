using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator
{
    public class Input
    {
        public InputValue _value;
        public string _name;

        public Input(string name, InputValue value)
        {
            _name = name;
            _value = value;
        }

        public enum InputValue
        {
            INPUT_LOW = 0,
            INPUT_HIGH = 1
        }

        public int ToInt()
        {
            return (int)_value;
        }
    }
    
}

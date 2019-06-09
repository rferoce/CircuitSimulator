using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator
{
    public class Probe {
        public ProbeValue _value;
        public string _name;

        public Probe(string name, ProbeValue value)
        {
            _name = name;
            _value = value;
        }

        public int ToInt()
        {
            return (int)_value;
        }
        public enum ProbeValue
        {
            PROBE_LOW = 0,
            PROBE_HIGH = 1
        }
    }
}

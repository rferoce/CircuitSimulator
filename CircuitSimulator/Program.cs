using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            CircuitBuilder circuitBuilder = new CircuitBuilder();
            CircuitSimulator circuitSimulator = new CircuitSimulator(circuitBuilder);
            circuitSimulator.Start();
        }
    }
}

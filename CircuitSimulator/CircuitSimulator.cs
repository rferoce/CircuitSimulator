using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator
{
    public class CircuitSimulator : Simulator
    {
        private ICircuitComponent _circuitComponent;
        private INumberConverter _numberConverter;

        public override void getDiscription()
        {
            base.getDiscription();
            Console.WriteLine("In detail, a CircuitSimulator calculates 2 numbers for you!");
        }
        public CircuitSimulator(ICircuitComponent circuitComponent)
        {
            _circuitComponent = circuitComponent;
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void Run()
        {
            throw new NotImplementedException();
        }
    }
}

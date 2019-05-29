using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator
{
    public class CircuitSimulator
    {
        private CircuitBuilder _circuitBuilder;

        public CircuitSimulator(CircuitBuilder circuitBuilder)
        {
            _circuitBuilder = circuitBuilder;
        }

        public void Start()
        {
            ArrayList files = new ArrayList();
            _circuitBuilder.Build(files);
        }
    }
}

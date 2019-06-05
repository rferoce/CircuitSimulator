using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator
{
    public class CircuitSimulatorBuilder
    {
        private CircuitSimulator _circuitSimulator;
        private CircuitBuilder _circuitBuilder;

        public CircuitSimulator GetPreparedCircuitSimulator()
        {
            return _circuitSimulator;
        }

        // TODO: check parameters?? 
        public void PrepareCircuitSimulator(File[] files)
        {
            CircuitSimulator _circuitSimulator = new CircuitSimulator();
            // Loop over file array and prepareCircuit for each file
            _circuitBuilder.prepareCircuit();
        }
    }
}

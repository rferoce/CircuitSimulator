using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator
{
    public class CircuitSimulatorBuilder
    {
        private CircuitBuilder _circuitBuilder;
        private CircuitSimulator _circuitSimulator;

        public CircuitSimulatorBuilder()
        {
            _circuitBuilder = new CircuitBuilder();
        }
        public CircuitSimulator GetPreparedCircuitSimulator()
        {
            return _circuitSimulator;
        }

        // TODO: check parameters?? 
        public void PrepareCircuitSimulator(string[] circuitFilePaths)
        {
            // this is an circuitComponent!! Not an array
            List<Circuit> circuits = new List<Circuit>();

            foreach (var circuitFilePath in circuitFilePaths)
            {
                // Loop over file array and prepareCircuit for each file
                _circuitBuilder.PrepareCircuit(circuitFilePath);
                Circuit circuit = _circuitBuilder.GetCircuit();
                circuits.Add(circuit);
            }
        }
    }
}

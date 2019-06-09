using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CircuitSimulator.Input;

namespace CircuitSimulator
{
    public class CircuitSimulatorBuilder
    {
        private string[] _circuitFilePaths;
        private CircuitSimulator _circuitSimulator;
        private List<Circuit> _circuits;
        public bool _incorrectSimulator { get; set; }

        public CircuitSimulatorBuilder()
        {
            _incorrectSimulator = false;
        }

        public CircuitSimulator GetPreparedCircuitSimulator()
        {
            return _circuitSimulator;
        }

        public void SetFilePaths(string[] filePaths)
        {
            _circuitFilePaths = filePaths;
        }

        public void PrepareCircuitSimulator(List<InputValue[]> inputs)
        {
            // Create new instances of circuitSimulator and it's circuits
            _circuitSimulator = new CircuitSimulator();
            _circuits = new List<Circuit>();

            // Reverse inputs
            List<InputValue[]> reversedInputs = ReverseInputs(inputs);

            // Prepare all circuits
            PrepareAllCircuits(reversedInputs); 
            if (_incorrectSimulator)
            {
                return;
            }

            _circuitSimulator.SetCircuits(_circuits);
        }

        private void PrepareAllCircuits(List<InputValue[]> reversedInputs)
        {
            int circuitFilePathsCounter = 0;
            foreach (var circuitFilePath in _circuitFilePaths)
            {
                CircuitBuilder circuitBuilder = new CircuitBuilder();
                // Loop over file array and prepareCircuit for each file
                circuitBuilder.PrepareCircuit(circuitFilePath);
                if (circuitBuilder._inCorrectCircuit)
                {
                    _incorrectSimulator = true;
                    return;
                }
                Circuit circuit = circuitBuilder.GetCircuit();

                // Initialize inputs of circuit
                InitializeInputs(circuitFilePathsCounter, circuit, reversedInputs);

                circuitFilePathsCounter++;
            }
        }

        private void InitializeInputs(int circuitFilePathsCounter, Circuit circuit, List<InputValue[]> reversedInputs)
        {
            List<Input> circuitInputs = circuit.GetInputs();
            Input cIn = circuitInputs.Where(c => c._name == "Cin").FirstOrDefault();

            // Change standard input values to user input values
            circuitInputs = ChangeCircuitInputs(circuitFilePathsCounter, circuitInputs, reversedInputs);

            circuitInputs.Add(cIn);
            circuit.SetInputs(circuitInputs);
            circuit.SetCircuitMediator(_circuitSimulator);
            
            // Save circuit
            _circuits.Add(circuit);
        }

        private List<Input> ChangeCircuitInputs(int circuitFilePathsCounter, List<Input> circuitInputs, List<InputValue[]> reversedInputs)
        {
            circuitInputs.RemoveAll(c => c._name == "Cin");

            for (int i = 0; i < circuitInputs.Count; i++)
            {
                if (circuitFilePathsCounter < reversedInputs[i].Length)
                {
                    circuitInputs[i]._value = reversedInputs[i][circuitFilePathsCounter];
                }
                else
                {
                    circuitInputs[i]._value = InputValue.INPUT_LOW;
                }
            }

            return circuitInputs;
        }

        private List<InputValue[]> ReverseInputs(List<InputValue[]> inputs)
        {
            List<InputValue[]> reversedInputs = new List<InputValue[]>();
            foreach (var input in inputs)
            {
                reversedInputs.Add(input.Reverse().ToArray());
            }

            return reversedInputs;
        }
    }
}

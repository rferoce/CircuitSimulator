using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CircuitSimulator.Input;

namespace CircuitSimulator
{
    public class CircuitSimulator : Simulator
    {
        private List<Circuit> _circuits;
        private List<int> _endResult;

        public CircuitSimulator()
        {
        }

        public override void Run()
        {
            _circuits[0].Run();
        }

        public override void Notify(Circuit sender, List<Probe> results)
        {
            // Find next circuit
            int indexOfNextCircuit = FindNextCircuit(sender);

            // Run next circuit or show end result
            if(indexOfNextCircuit < _circuits.Count())
            {
                RunNextCircuit(indexOfNextCircuit, results);
            }
            else
            {
                ShowEndResult();
            }
        }

        private void RunNextCircuit(int indexOfNextCircuit, List<Probe> results)
        {
            // Convert probes To inputs
            ConvertProbesToInputs(indexOfNextCircuit, results);

            // Run next circuit
            _circuits[indexOfNextCircuit].Run();
        }

        private void ConvertProbesToInputs(int indexOfNextCircuit, List<Probe> results)
        {
            List<Input> inputs = new List<Input>();
            foreach (var probe in results)
            {
                if (probe._name == "Cout")
                {
                    inputs.Add(new Input("Cin", (InputValue)probe._value));
                }
            }
            _circuits[indexOfNextCircuit].SetInputs(inputs);
        }

        private int FindNextCircuit(Circuit notifiedCircuit)
        {
            int index = 0;
            foreach (var circuit in _circuits)
            {
                if (circuit == notifiedCircuit)
                {
                    index++;
                    break;
                }
                index++;
            }
            return index;
        }

        private void ShowEndResult()
        {
            // Initialize end result
            InitializeEndResult();

            // Convert end result to binary number
            string binaryResult = string.Join("", _endResult);

            // Convert end result to numerical number
            string numericalResult = Convert.ToInt32(binaryResult, 2).ToString();

            // Show end result
            ConsoleWriterSingleton.Instance.ShowSimulatorEndresult(binaryResult, numericalResult);
        }

        private void InitializeEndResult()
        {
            _endResult = new List<int>();
            
            // Add S Probe values to end result
            AddProbeValuesToEndresult();

            // Add Cout Probe value of last executed circuit to end result
            AddLastCoutValueToEndResult();

            // Set end result in correct order
            _endResult.Reverse();
        }

        private void AddLastCoutValueToEndResult()
        {
            Circuit lastExecutedCircuit = _circuits[_circuits.Count() - 1];

            foreach (var probe in lastExecutedCircuit.GetProbes())
            {
                if (probe._name == "Cout")
                {
                    int probeInt = probe.ToInt();
                    if (probeInt != 0)
                        _endResult.Add(probe.ToInt());
                }
            }
        }

        private void AddProbeValuesToEndresult()
        {
            int index = 0;
            foreach (var circuit in _circuits)
            {
                // Add S Probe values to end result
                foreach (var probe in circuit.GetProbes())
                {
                    if (probe._name == "S")
                    {
                        _endResult.Add(probe.ToInt());
                    }
                }
                index++;
            }
        }

        public void SetCircuits(List<Circuit> circuits)
        {
            _circuits = circuits;
        }
    }
}

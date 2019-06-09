using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CircuitSimulator.Input;
using static CircuitSimulator.Probe;

namespace CircuitSimulator
{
    public class Circuit : ICircuitMediator
    {
        private Dictionary<Input, INodeComponent> _inputsCircuitMapping;
        private Dictionary<INodeComponent, Probe> _probesCircuitMapping;
        private Dictionary<INodeComponent, Probe> _finalProbes;
        private INodeVisitor _nodeEnglishDescriptionVisitor;
        private INodeVisitor _nodeDrawVisitor;
        private List<Probe> _results;
        private int _expectedProbeAmount = 2;
        public ISimulatorMediator _simulatorMediator { get; set; }

        public Circuit()
        {
            // Initialize Circuit 
            InitializeCircuit();
        }

        private void InitializeCircuit()
        {
            _finalProbes = new Dictionary<INodeComponent, Probe>();
            _nodeEnglishDescriptionVisitor = new NodeEnglishDescriptionVisitor();
            _nodeDrawVisitor = new NodeDrawVisitor();
            _results = new List<Probe>();
        }

        public void SetCircuitMediator(ISimulatorMediator simulatorMediator)
        {
            _simulatorMediator = simulatorMediator;
        }

        public void SetMappings(Dictionary<Input, INodeComponent> inputsCircuitMapping, Dictionary<INodeComponent, Probe> probesCircuitMapping)
        {
            _inputsCircuitMapping = inputsCircuitMapping;
            _probesCircuitMapping = probesCircuitMapping;
        }

        public List<Input> GetInputs()
        {
            List<Input> inputs = new List<Input>();
            foreach (var inputCircuitMapping in _inputsCircuitMapping)
            {
                inputs.Add(inputCircuitMapping.Key);
            }

            return inputs;
        }

        public void SetInputs(List<Input> inputs)
        {
            foreach (var input in inputs)
            {
                foreach (var inputCircuitMapping in _inputsCircuitMapping)
                {
                    if (input._name == inputCircuitMapping.Key._name)
                    {
                        inputCircuitMapping.Key._value = input._value;
                    }
                }
            }
        }

        public List<Probe> GetProbes()
        {
            return _results;
        }

        public void Run()
        {
            foreach (var inputCircuitMapping in _inputsCircuitMapping)
            {
                // Show Input
                ConsoleWriterSingleton.Instance.ShowInput(inputCircuitMapping.Key._name, inputCircuitMapping.Key.ToInt());

                // Show next of input
                _inputsCircuitMapping[inputCircuitMapping.Key].FindNextNodes(_inputsCircuitMapping[inputCircuitMapping.Key]);

                // Run next
                RunINodeComponent(_inputsCircuitMapping[inputCircuitMapping.Key], inputCircuitMapping.Key._value);
            }
        }

        public void RunINodeComponent(INodeComponent nodeComposite, InputValue inputValue)
        {
            bool foundNodeChild = false;

            foreach (var child in nodeComposite.Children)
            {
                // Check whether child is Node
                if (child.CanCalculate())
                {
                    child.AddInput(inputValue);
                    foundNodeChild = true;
                }
            }

            // If no nodeChild is found, run children folder
            if (!foundNodeChild)
            {
                foreach (var child in nodeComposite.Children)
                {
                    RunINodeComponent(child, inputValue);
                }
            }
        }

        public void Notify(Node sender, InputValue result)
        {
            // Show Notify
            ShowNotify(sender, (int)result);

            // Show next node
            sender.ChildrenToString();

            foreach (var child in sender.Parent.Children)
            {
                // Check if sender is connected to Probe
                foreach (var grandChild in child.Children)
                {
                    CheckGrandChildIsProbe(grandChild, (ProbeValue)result);
                }

                // Find next node
                if (!child.CanCalculate())
                {
                    RunINodeComponent(child, result);
                }
            }
        }

        private void CheckGrandChildIsProbe(INodeComponent grandChild, ProbeValue result)
        {
            if (_probesCircuitMapping.ContainsKey(grandChild))
            {
                // Show next (is probe)
                ConsoleWriterSingleton.Instance.ShowNextProbe(_probesCircuitMapping[grandChild]._name);

                // Set result of probe
                _probesCircuitMapping[grandChild]._value = result;

                // Add probe to final probes mapping
                _finalProbes.Add(grandChild, _probesCircuitMapping[grandChild]);

                // Show probe (intermediate) result
                ConsoleWriterSingleton.Instance.ShowProbeResult(_probesCircuitMapping[grandChild]._name, _probesCircuitMapping[grandChild].ToInt());

                // Add probe as circuit result
                AddResult(_probesCircuitMapping[grandChild]);
            }
        }

        private void ShowNotify(Node sender, int result)
        {
            ConsoleWriterSingleton.Instance.ShowFirstComponentOfNodeResult();
            sender.Accept(_nodeEnglishDescriptionVisitor);
            ConsoleWriterSingleton.Instance.ShowMiddleComponentOfNodeResult();
            sender.Accept(_nodeDrawVisitor);
            ConsoleWriterSingleton.Instance.ShowResultComponentOfNodeResult(result);
        }

        private void AddResult(Probe result)
        {
            _results.Add(result);

            if(_results.Count() == _expectedProbeAmount)
            {
                ShowCircuitEndResult();
                _simulatorMediator.Notify(this, _results);
            }
        }

        private void ShowCircuitEndResult()
        {
            // Initialize result
            int[] results = InitializeResult();

            // Convert result to binary 
            string endResult = string.Join("", results);

            // Show result of circuit
            ConsoleWriterSingleton.Instance.ShowCircuitResult(endResult);
        }

        private int[] InitializeResult()
        {
            int[] results = new int[2];
            foreach (var probe in _results)
            {
                if (probe._name == "Cout")
                {
                    results[0] = probe.ToInt();
                }

                if (probe._name == "S")
                {
                    results[1] = probe.ToInt();
                }
            }

            return results;
        }
    }
}

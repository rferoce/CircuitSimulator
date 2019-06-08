using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CircuitSimulator.Input;
using static CircuitSimulator.Probe;

namespace CircuitSimulator
{
    public class Circuit : ICircuitComponent, ICircuitMediator
    {
        private Dictionary<Input, INodeComponent> _inputsCircuitMapping;
        private Dictionary<INodeComponent, Probe> _probesCircuitMapping;
        private Dictionary<INodeComponent, Probe> _finalProbes;
        private INodeVisitor _nodeEnglishDescriptionVisitor;
        private INodeVisitor _nodeDrawVisitor;
        private string _endResult;

        public Circuit()
        {
            _finalProbes = new Dictionary<INodeComponent, Probe>();
            _nodeEnglishDescriptionVisitor = new NodeEnglishDescriptionVisitor();
            _nodeDrawVisitor = new NodeDrawVisitor();
        }

        public void SetMappings(Dictionary<Input, INodeComponent> inputsCircuitMapping, Dictionary<INodeComponent, Probe> probesCircuitMapping)
        {
            _inputsCircuitMapping = inputsCircuitMapping;
            _probesCircuitMapping = probesCircuitMapping;
        }

        public ICircuitComponent Parent
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void Run()
        {
            foreach (var inputCircuitMapping in _inputsCircuitMapping)
            {
                
                Console.WriteLine("Input with name {0}, has next: ", inputCircuitMapping.Key._name);
                _inputsCircuitMapping[inputCircuitMapping.Key].FindNextNodes(_inputsCircuitMapping[inputCircuitMapping.Key]);
                Console.WriteLine();

                RunCircuit(_inputsCircuitMapping[inputCircuitMapping.Key], inputCircuitMapping.Key._value);
            }

            Console.WriteLine("End result is {0}", _endResult);
        }

        public void RunCircuit(INodeComponent nodeComposite, InputValue inputValue)
        {
            bool foundNodeChild = false;

            foreach (var child in nodeComposite.Children)
            {
                if (child.CanCalculate())
                {
                    child.AddInput(inputValue);
                    foundNodeChild = true;
                }
            }

            if (!foundNodeChild)
            {
                foreach (var child in nodeComposite.Children)
                {
                    RunCircuit(child, inputValue);
                }
            }
        }

        public int CalculateDelayTime()
        {
            throw new NotImplementedException();
        }

        public void Notify(Node sender, InputValue result)
        {
            Console.Write("INFO - ");
            sender.Accept(_nodeEnglishDescriptionVisitor);
            Console.Write(", has construction ");
            sender.Accept(_nodeDrawVisitor);
            Console.Write(", calculated result: {0}", (int)result);
            Console.WriteLine();

            sender.ChildrenToString();

            foreach (var child in sender.Parent.Children)
            {
                // check if one of children of child is a probe
                foreach (var childChild in child.Children)
                {
                    // Check if sender is connected to ouput!
                    if (_probesCircuitMapping.ContainsKey(childChild))
                    {
                        Console.WriteLine("Next node is: {0}", _probesCircuitMapping[childChild]._name);
                        Console.WriteLine();

                        _probesCircuitMapping[childChild]._value = (ProbeValue)result;
                        _finalProbes.Add(childChild, _probesCircuitMapping[childChild]);

                        // Show intermediate end result
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("INFO - Probe with name {0} ends with value {1}", _probesCircuitMapping[childChild]._name, _probesCircuitMapping[childChild].ToInt());
                        Console.ResetColor();

                        _endResult = _probesCircuitMapping[childChild].ToInt().ToString() + _endResult;
                    }
                }

                if (!child.CanCalculate())
                {
                    // show intermediate result
                    Console.WriteLine();
                    RunCircuit(child, result);
                }
            }
        }
    }
}

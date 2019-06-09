using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CircuitSimulator.Input;
using static CircuitSimulator.Probe;

namespace CircuitSimulator
{
    public class CircuitBuilder
    {
        private FileReader _fileReader;
        private StringParser _stringParser;
        public bool _inCorrectCircuit { get; set; }

        private Dictionary<string, InputValue> _inputDictionary;
        private Dictionary<string, ProbeValue> _probeDictionary;

        private List<KeyValuePair<string, string[]>> _checkedEdges;

        private List<Input> _inputs;
        private List<Probe> _probes;
        private Dictionary<string, Node> _nodeMapping;
        private Dictionary<string, string[]> _edgeMapping;

        private Dictionary<Input, INodeComponent> _inputMapping;
        private Dictionary<INodeComponent, Probe> _probeMapping;
        private Circuit _circuit;

        public CircuitBuilder()
        {
            _fileReader = new FileReader();
            _stringParser = new StringParser();
            _inCorrectCircuit = false;

            _inputDictionary = new Dictionary<string, InputValue> {
                    { "INPUT_LOW", InputValue.INPUT_LOW},
                    { "INPUT_HIGH", InputValue.INPUT_HIGH },
                };

            _probeDictionary = new Dictionary<string, ProbeValue> {
                    { "PROBE", ProbeValue.PROBE_LOW},
                };

            _checkedEdges = new List<KeyValuePair<string, string[]>>();

            _inputs = new List<Input>();
            _probes = new List<Probe>();
            _nodeMapping = new Dictionary<string, Node>();
            _edgeMapping = new Dictionary<string, string[]>();

            _inputMapping = new Dictionary<Input, INodeComponent>();
            _probeMapping = new Dictionary<INodeComponent, Probe>();

            NodeFactorySingleton.Instance.RegisterNode("OR", new OR());
            NodeFactorySingleton.Instance.RegisterNode("NOT", new NOT());
            NodeFactorySingleton.Instance.RegisterNode("NAND", new NAND());
            NodeFactorySingleton.Instance.RegisterNode("AND", new AND());
            NodeFactorySingleton.Instance.RegisterNode("XOR", new XOR());
            NodeFactorySingleton.Instance.RegisterNode("NOR", new NOR());
        }

        public void PrepareCircuit(string filePath)
        {
            // Read file and create mapping of strings
            string fileString = _fileReader.ReadFile(filePath);
            Dictionary<string, string[]>[] fileStringMapping = _stringParser.readFileString(fileString);
            Dictionary<string, string[]> componentMapping = fileStringMapping[0];
            Dictionary<string, string[]> edgeMapping = fileStringMapping[1];

            bool invalidCircuit = CheckCircuit(componentMapping, edgeMapping);
            if (invalidCircuit)
            {
                _inCorrectCircuit = true;
                return;
            }

            // step 1: Create Mappings
            CreateComponentMapping(componentMapping);
            CreateEdgeMapping(edgeMapping);

            // step 2: Create Circuit
            CreateCircuit();
        }

        public Circuit GetCircuit()
        {
            return _circuit;
        }

        public bool CheckCircuit(Dictionary<string, string[]> componentMapping, Dictionary<string, string[]> edgeMapping)
        {
            bool circuitContainsUnlinkedNode = CheckUnlinkedNode(edgeMapping, componentMapping);
            bool circuitContainsInfinteLoop = CheckInfiniteLoop(edgeMapping, componentMapping);

            return (circuitContainsUnlinkedNode || circuitContainsInfinteLoop); 
        }

        public bool CheckInfiniteLoop(Dictionary<string, string[]> edgeMapping, Dictionary<string, string[]> componentMapping)
        {
            foreach (KeyValuePair<string, string[]> edge in edgeMapping)
            {
                _checkedEdges = new List<KeyValuePair<string, string[]>>();
                if (componentMapping[edge.Key][0] == "INPUT_HIGH" || componentMapping[edge.Key][0] == "INPUT_LOW")
                {
                    _checkedEdges.Add(edge);
                    if (CheckEdge(edge, edgeMapping, componentMapping))
                    {
                        return true;
                    }
                } 
            }

            return false;
        }

        public bool CheckEdge(KeyValuePair<string, string[]> edge, Dictionary<string, string[]> edgeMapping, Dictionary<string, string[]> componentMapping)
        {
            foreach (var edgeValue in edge.Value)
            {
                if (edgeMapping.ContainsKey(edgeValue))
                {
                    if (componentMapping[edgeMapping[edgeValue][0]][0] != "PROBE")
                    {
                        KeyValuePair<string, string[]> nextEdge = new KeyValuePair<string, string[]>(edgeValue, edgeMapping[edgeValue]);
                        if (_checkedEdges.Contains(nextEdge))
                        {
                            ConsoleWriterSingleton.Instance.ShowInfiniteLoopMessage(nextEdge.Key);
                            return true;
                        }

                        _checkedEdges.Add(nextEdge);
                        if (CheckEdge(nextEdge, edgeMapping, componentMapping))
                            return true;
                    }
                    else
                    {
                        _checkedEdges = new List<KeyValuePair<string, string[]>>();
                    }
                }
            }
            return false;
        }

        public bool CheckUnlinkedNode(Dictionary<string, string[]> edgeMapping, Dictionary<string, string[]> componentMapping)
        {
            foreach (KeyValuePair<string, string[]> entry in edgeMapping)
            {
                foreach (string value in entry.Value)
                {
                    bool valueIsProbe = false;
                    foreach (var componentMappingValue in componentMapping[value])
                    {
                        if (componentMappingValue == "PROBE")
                        {
                            valueIsProbe = true;
                            break;
                        }
                    }

                    if (valueIsProbe)
                    {
                        continue;
                    }

                    if (!edgeMapping.ContainsKey(value))
                    {
                        ConsoleWriterSingleton.Instance.ShowUnLinkedNodeMessage(value);
                        return true;
                    }
                }
            }
            return false;
        }

        public void CreateComponentMapping(Dictionary<string, string[]> fileStringMapping)
        {
            _circuit = new Circuit();

            foreach (KeyValuePair<string, string[]> entry in fileStringMapping)
            {
                if (entry.Value[0] == "INPUT_LOW" || entry.Value[0] == "INPUT_HIGH")
                {
                    // create mapping of Inputs
                    _inputs.Add(new Input(entry.Key, _inputDictionary[entry.Value[0]]));
                    continue;
                }

                if (entry.Value[0] == "PROBE")
                {
                    // create mapping of Probes
                    _probes.Add(new Probe(entry.Key, _probeDictionary[entry.Value[0]]));
                    continue;
                }

                if (entry.Value.Length == 1)
                {
                    // create mapping of Nodes
                    Node createdNode = NodeFactorySingleton.Instance.CreateNode(entry.Value[0]);
                    createdNode._name = entry.Key;
                    createdNode.SetCircuitMediator(_circuit);

                    if (createdNode != null)
                    {
                        _nodeMapping.Add(entry.Key, createdNode);
                        continue;
                    }
                }
            }
        }

        public void CreateEdgeMapping(Dictionary<string, string[]> fileStringMapping)
        {
            foreach (KeyValuePair<string, string[]> entry in fileStringMapping)
            {
                // create mapping of the edges
                _edgeMapping.Add(entry.Key, entry.Value);
            }
        }

        public void CreateCircuit()
        {
            foreach (var edge in _edgeMapping)
            {
                // check if entry is input (existing key in inputMapping) should be the last check!
                foreach (var input in _inputs)
                {
                    if (edge.Key == input._name)
                    {
                        INodeComponent edgeValuesWrapComposite = CreateNodeComponent(edge);
                        _inputMapping.Add(input, edgeValuesWrapComposite);
                    }
                }
            }

            _circuit.SetMappings(_inputMapping, _probeMapping);
        }

        public INodeComponent CreateNodeComponent(KeyValuePair<string, string[]> edge)
        {
            // this is array to wrap edgeMapping values 
            NodeComposite edgeValuesWrapComposite = new NodeComposite();
            foreach (var value in edge.Value)
            {
                NodeComposite nodeComposite = new NodeComposite();

                if (_nodeMapping.ContainsKey(value))
                {
                    // add Node object to composite
                    nodeComposite.AddChild(_nodeMapping[value]);
                } else
                {
                    foreach (var probe in _probes)
                    {
                        if (probe._name == value)
                        {
                            _probeMapping.Add(nodeComposite, probe);
                            break;
                        }
                    }
                }
                

                // add child composite
                if (_edgeMapping.ContainsKey(value))
                {
                    KeyValuePair<string, string[]> childEdge = new KeyValuePair<string, string[]>(value, _edgeMapping[value]);
                    INodeComponent childEdgeValuesWrapComposite = CreateNodeComponent(childEdge);
                    nodeComposite.AddChild(childEdgeValuesWrapComposite);
                }

                edgeValuesWrapComposite.AddChild(nodeComposite);
            }

            return edgeValuesWrapComposite;
        }

    }
}
    
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

        private Dictionary<string, InputValue> _inputDictionary;
        private Dictionary<string, ProbeValue> _probeDictionary;

        private List<KeyValuePair<string, string[]>> checkedEdges = new List<KeyValuePair<string, string[]>>();

        private List<Input> inputs = new List<Input>();
        private List<Probe> probes = new List<Probe>();
        private Dictionary<string, Node> nodeMapping = new Dictionary<string, Node>();
        private Dictionary<string, string[]> edgeMapping = new Dictionary<string, string[]>();

        private Dictionary<Input, INodeComponent> inputMapping = new Dictionary<Input, INodeComponent>();
        private Dictionary<INodeComponent, Probe> probeMapping = new Dictionary<INodeComponent, Probe>();
        private Circuit _circuit;

        public CircuitBuilder()
        {
            _fileReader = new FileReader();
            _stringParser = new StringParser();

            NodeFactorySingleton.Instance.RegisterNode("OR", new OR());
            NodeFactorySingleton.Instance.RegisterNode("NOT", new NOT());
            NodeFactorySingleton.Instance.RegisterNode("NAND", new NAND());
            NodeFactorySingleton.Instance.RegisterNode("AND", new AND());
            NodeFactorySingleton.Instance.RegisterNode("XOR", new XOR());
            NodeFactorySingleton.Instance.RegisterNode("NOR", new NOR());

            _inputDictionary = new Dictionary<string, InputValue> {
                    { "INPUT_LOW", InputValue.INPUT_LOW},
                    { "INPUT_HIGH", InputValue.INPUT_HIGH },
                };

            _probeDictionary = new Dictionary<string, ProbeValue> {
                    { "PROBE", ProbeValue.PROBE_LOW},
                };

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
                Console.WriteLine("Press any key to exit");
                Console.ReadLine();
                Environment.Exit(0);
                return;
            }

            // step 1: Create Mappings
            CreateComponentMapping(componentMapping);
            CreateEdgeMapping(edgeMapping);

            // step 2: Create Circuit
            CreateCircuit();

            // TODO: remove this quick test
            _circuit.Run();
            Console.ReadLine();
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
                checkedEdges = new List<KeyValuePair<string, string[]>>();
                if (componentMapping[edge.Key][0] == "INPUT_HIGH" || componentMapping[edge.Key][0] == "INPUT_LOW")
                {
                    checkedEdges.Add(edge);
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
                        if (checkedEdges.Contains(nextEdge))
                        {
                            Console.WriteLine("ERROR: Found infinite loop. Node with name {0} was called multiple times", nextEdge.Key);
                            return true;
                        }

                        checkedEdges.Add(nextEdge);
                        if (CheckEdge(nextEdge, edgeMapping, componentMapping))
                            return true;
                    }
                    else
                    {
                        checkedEdges = new List<KeyValuePair<string, string[]>>();
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
                        Console.WriteLine("ERROR: Node with name {0} is not linked to next Node!", value);
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
                    inputs.Add(new Input(entry.Key, _inputDictionary[entry.Value[0]]));
                    continue;
                }

                if (entry.Value[0] == "PROBE")
                {
                    // create mapping of Probes
                    probes.Add(new Probe(entry.Key, _probeDictionary[entry.Value[0]]));
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
                        nodeMapping.Add(entry.Key, createdNode);
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
                edgeMapping.Add(entry.Key, entry.Value);
            }
        }

        public void CreateCircuit()
        {
            foreach (var edge in edgeMapping)
            {
                // check if entry is input (existing key in inputMapping) should be the last check!
                foreach (var input in inputs)
                {
                    if (edge.Key == input._name)
                    {
                        INodeComponent edgeValuesWrapComposite = CreateNodeComponent(edge);
                        inputMapping.Add(input, edgeValuesWrapComposite);
                    }
                }
            }

            _circuit.SetMappings(inputMapping, probeMapping);
        }

        public INodeComponent CreateNodeComponent(KeyValuePair<string, string[]> edge)
        {
            // this is array to wrap edgeMapping values 
            NodeComposite edgeValuesWrapComposite = new NodeComposite();
            foreach (var value in edge.Value)
            {
                NodeComposite nodeComposite = new NodeComposite();

                if (nodeMapping.ContainsKey(value))
                {
                    // add Node object to composite
                    nodeComposite.AddChild(nodeMapping[value]);
                } else
                {
                    foreach (var probe in probes)
                    {
                        if (probe._name == value)
                        {
                            probeMapping.Add(nodeComposite, probe);
                            break;
                        }
                    }
                }
                

                // add child composite
                if (edgeMapping.ContainsKey(value))
                {
                    KeyValuePair<string, string[]> childEdge = new KeyValuePair<string, string[]>(value, edgeMapping[value]);
                    INodeComponent childEdgeValuesWrapComposite = CreateNodeComponent(childEdge);
                    nodeComposite.AddChild(childEdgeValuesWrapComposite);
                }

                edgeValuesWrapComposite.AddChild(nodeComposite);
            }

            return edgeValuesWrapComposite;
        }

    }
}
    
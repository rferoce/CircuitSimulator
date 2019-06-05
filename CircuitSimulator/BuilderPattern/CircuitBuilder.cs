using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator
{
    public class CircuitBuilder
    {
        private FileReader _fileReader;
        private StringParser _stringParser;

        public CircuitBuilder()
        {
            _fileReader = new FileReader();
            _stringParser = new StringParser();

            NodeFactorySingleton.Instance.addNodeType("OR", typeof(OR));
            NodeFactorySingleton.Instance.addNodeType("NOT", typeof(NOT));
            NodeFactorySingleton.Instance.addNodeType("NAND", typeof(NAND));
            NodeFactorySingleton.Instance.addNodeType("AND", typeof(AND));
            NodeFactorySingleton.Instance.addNodeType("XOR", typeof(XOR));
            NodeFactorySingleton.Instance.addNodeType("NOR", typeof(NOR));

        }

        public void PrepareCircuit(string filePath)
        {
            string fileString = _fileReader.ReadFile(filePath);
            Dictionary<string, string[]> mapping = _stringParser.readFileString(fileString);
        }

        public void GetCircuit()
        {

        }

    }
}
    
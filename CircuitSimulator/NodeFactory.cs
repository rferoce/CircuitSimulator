using CircuitSimulator.Model.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator
{
    public class NodeFactory
    {
        public Node createNode(string node)
        {
            if(node.Equals("AND")) { return new ANDNode(); }
            if(node.Equals("NAND")) { return new NANDNode(); }
            if (node.Equals("OR")) { return new ORNode(); }
            if (node.Equals("XOR")) { return new XORNode(); }
            if (node.Equals("NOR")) { return new NORNode(); }
            if (node.Equals("NOT")) { return new NOTNode(); }

            return null;
        }
    }
}

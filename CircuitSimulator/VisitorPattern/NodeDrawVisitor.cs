using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator
{
    public class NodeDrawVisitor : INodeVisitor
    {
        public void VisitNode(NOR node)
        {
            Console.Write(">|>=1|.--");
        }

        public void VisitNode(OR node)
        {
            Console.Write(">|>=1|--");
        }

        public void VisitNode(XOR node)
        {
            Console.Write(">|=1|--");
        }

        public void VisitNode(NOT node)
        {
            Console.Write("--|>.--");
        }

        public void VisitNode(NAND node)
        {
            Console.Write(">|&|.--");
        }

        public void VisitNode(AND node)
        {
            Console.Write(">|&|--");
        }
    }
}

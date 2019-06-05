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
            Console.WriteLine("-/-");
        }

        public void VisitNode(OR node)
        {
            Console.WriteLine("-/+");
        }

        public void VisitNode(XOR node)
        {
            Console.WriteLine("+/-");
        }

        public void VisitNode(NOT node)
        {
            Console.WriteLine("-/-");
        }

        public void VisitNode(NAND node)
        {
            Console.WriteLine("-/-");
        }

        public void VisitNode(AND node)
        {
            Console.WriteLine("+/+");
        }
    }
}

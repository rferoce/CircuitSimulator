using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator
{
    public class NodeEnglishDescriptionVisitor : INodeVisitor
    {
        public void VisitNode(NOR node)
        {
            Console.WriteLine("This is my description: {0}", node.ToString());
        }

        public void VisitNode(OR node)
        {
            Console.WriteLine("This is my description: {0}", node.ToString());
        }

        public void VisitNode(XOR node)
        {
            Console.WriteLine("This is my description: {0}", node.ToString());
        }

        public void VisitNode(NOT node)
        {
            Console.WriteLine("This is my description: {0}", node.ToString());
        }

        public void VisitNode(NAND node)
        {
            Console.WriteLine("This is my description: {0}", node.ToString());
        }

        public void VisitNode(AND node)
        {
            Console.WriteLine("This is my description: {0}", node.ToString());
        }
    }
}

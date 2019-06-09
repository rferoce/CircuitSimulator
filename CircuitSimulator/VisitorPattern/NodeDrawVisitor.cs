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
            ConsoleWriterSingleton.Instance.DrawNode(">|>=1|.--");
        }

        public void VisitNode(OR node)
        {
            ConsoleWriterSingleton.Instance.DrawNode(">|>=1|--");
        }

        public void VisitNode(XOR node)
        {
            ConsoleWriterSingleton.Instance.DrawNode(">|=1|--");
        }

        public void VisitNode(NOT node)
        {
            ConsoleWriterSingleton.Instance.DrawNode("--|>.--");
        }

        public void VisitNode(NAND node)
        {
            ConsoleWriterSingleton.Instance.DrawNode(">|&|.--");
        }

        public void VisitNode(AND node)
        {
            ConsoleWriterSingleton.Instance.DrawNode(">|&|--");
        }
    }
}

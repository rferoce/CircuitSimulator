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
            ConsoleWriterSingleton.Instance.ShowEnglishNodeDescription("Node with name {0} is a NOR Node", node._name);
        }

        public void VisitNode(OR node)
        {
            ConsoleWriterSingleton.Instance.ShowEnglishNodeDescription("Node with name {0} is an OR Node", node._name);
        }

        public void VisitNode(XOR node)
        {
            ConsoleWriterSingleton.Instance.ShowEnglishNodeDescription("Node with name {0} is a XOR Node", node._name);
        }

        public void VisitNode(NOT node)
        {
            ConsoleWriterSingleton.Instance.ShowEnglishNodeDescription("Node with name {0} is a NOT Node", node._name);
        }

        public void VisitNode(NAND node)
        {
            ConsoleWriterSingleton.Instance.ShowEnglishNodeDescription("Node with name {0} is a NAND Node", node._name);
        }

        public void VisitNode(AND node)
        {
            ConsoleWriterSingleton.Instance.ShowEnglishNodeDescription("Node with name {0} is an AND Node", node._name);
        }
    }
}

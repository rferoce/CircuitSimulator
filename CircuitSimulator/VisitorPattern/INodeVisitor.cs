using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator
{
    public interface INodeVisitor
    {
        void VisitNode(AND node);
        void VisitNode(NAND node);
        void VisitNode(NOR node);
        void VisitNode(NOT node);
        void VisitNode(OR node);
        void VisitNode(XOR node);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CircuitSimulator.Input;

namespace CircuitSimulator
{
    public interface INodeComponent
    {
        INodeComponent Parent { get; set; }
        List<INodeComponent> Children { get; }
        void AddInput(InputValue input);
        bool CanCalculate();
        void Accept(INodeVisitor visitor);
        void FindNextNodes(INodeComponent nodeComposite);
    }
}

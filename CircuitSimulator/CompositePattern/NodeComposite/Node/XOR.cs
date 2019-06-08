using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CircuitSimulator.Input;

namespace CircuitSimulator
{
    public class XOR : Node
    {
        public XOR()
        {
            InputsAmount = 2;
        }

        public override void Calculate()
        {
            InputValue result = InputValue.INPUT_HIGH;

            if (_inputs[0] == _inputs[1])
                result = InputValue.INPUT_LOW;

            base._circuitMediator.Notify(this, result);
        }

        public override Node Clone()
        {
            return new XOR() { InputsAmount = this.InputsAmount };
        }

        public override void Accept(INodeVisitor visitor)
        {
            visitor.VisitNode(this);
        }
    }
}

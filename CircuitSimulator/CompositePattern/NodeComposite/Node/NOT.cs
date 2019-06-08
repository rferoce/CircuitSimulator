using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CircuitSimulator.Input;

namespace CircuitSimulator
{
    public class NOT : Node
    {
       public NOT()
        {
            InputsAmount = 1;
        }

        public override void Calculate()
        {
            InputValue result = InputValue.INPUT_LOW;

            if (_inputs[0] == InputValue.INPUT_LOW)
            {
                result = InputValue.INPUT_HIGH;
            } 

            base._circuitMediator.Notify(this, result);
        }

        public override Node Clone()
        {
            return new NOT() { InputsAmount = InputsAmount };
        }

        public override void Accept(INodeVisitor visitor)
        {
            visitor.VisitNode(this);
        }
    }
}

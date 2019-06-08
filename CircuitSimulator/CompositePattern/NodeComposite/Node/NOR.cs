using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CircuitSimulator.Input;

namespace CircuitSimulator
{
    public class NOR : Node
    {
        private bool _notified;
        public NOR()
        {
            _notified = false;
            InputsAmount = 1;
        }

        public override void AddInput(InputValue input)
        {
            if (!_notified && input == InputValue.INPUT_HIGH)
            {
                _notified = true;
                base._circuitMediator.Notify(this, InputValue.INPUT_LOW);
            }
            else
            {
                base.AddInput(input);
            }
        }
        public override void Calculate()
        {
            InputValue result = InputValue.INPUT_LOW;

            if (_inputs[0] == InputValue.INPUT_LOW && _inputs[1] == InputValue.INPUT_LOW)
                result = InputValue.INPUT_HIGH;

            if(!_notified)
                base._circuitMediator.Notify(this, result);
        }

        public override Node Clone()
        {
            return new NOR() { InputsAmount = InputsAmount };
        }

        public override void Accept(INodeVisitor visitor)
        {
            visitor.VisitNode(this);
        }
    }
}

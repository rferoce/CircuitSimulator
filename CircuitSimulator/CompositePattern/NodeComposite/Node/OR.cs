using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CircuitSimulator.Input;

namespace CircuitSimulator
{
    public class OR : Node
    {
        private bool _notified;

        public OR()
        {
            _notified = false;
            InputsAmount = 2;
        }

        public override void AddInput(InputValue input)
        {
            if (!_notified && input == InputValue.INPUT_HIGH)
            {
                _notified = true;
                base._circuitMediator.Notify(this, InputValue.INPUT_HIGH);
            }
            else
            {
                base.AddInput(input);
            }
        }

        public override void Calculate()
        {
            InputValue result = InputValue.INPUT_LOW;

            foreach (var input in _inputs)
            {
                if (input == InputValue.INPUT_HIGH)
                {
                    result = InputValue.INPUT_HIGH;
                    break;
                }
            }

            if(!_notified)
                base._circuitMediator.Notify(this, result);
        }

        public override Node Clone()
        {
            return new OR() { InputsAmount = InputsAmount };
        }

        public override void Accept(INodeVisitor visitor)
        {
            visitor.VisitNode(this);
        }
    }
}

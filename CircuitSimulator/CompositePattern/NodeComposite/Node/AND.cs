﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CircuitSimulator.Input;

namespace CircuitSimulator
{
    public class AND : Node
    {
        private bool _notified;
        public AND()
        {
            _notified = false;
            InputsAmount = 2;
        }

        public override void AddInput(InputValue input)
        {
            if (!_notified && input == InputValue.INPUT_LOW)
            {
                _notified = true;
                base._circuitMediator.Notify(this, InputValue.INPUT_LOW);
            } else
            {
                base.AddInput(input);
            }
        }
        public override void Calculate()
        {
            InputValue result = InputValue.INPUT_LOW;
            if (_inputs[0] == Input.InputValue.INPUT_HIGH && _inputs[1] == Input.InputValue.INPUT_HIGH)
                result = InputValue.INPUT_HIGH;

            if(!_notified)
                base._circuitMediator.Notify(this, result);
        }

        public override Node Clone()
        {
            return new AND() { InputsAmount = InputsAmount };
        }

        public override void Accept(INodeVisitor visitor)
        {
            visitor.VisitNode(this);
        }
    }
}

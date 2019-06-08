using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CircuitSimulator.Input;

namespace CircuitSimulator
{
    public interface ICircuitMediator
    {
        void Notify(Node sender, InputValue result);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator
{
    public class Circuit : CircuitComponent, ICircuitMediator
    {
        private Input[] _input;
        private Probe[] _probe;
        public int CalculateDelayTime()
        {
            throw new NotImplementedException();
        }

        public void Notify()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator
{
    public class Circuit : ICircuitComponent, ICircuitMediator
    {
        private Input[] _input;
        private Probe[] _probe;

        public ICircuitComponent Parent
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int CalculateDelayTime()
        {
            throw new NotImplementedException();
        }

        public void notify(Node sender, string message)
        {
            throw new NotImplementedException();
        }
    }
}

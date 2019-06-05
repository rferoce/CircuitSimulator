using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator
{
    public class _CircuitComposite : ICircuitComponent
    {
        private ICircuitComponent _circuitComponent;

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
    }
}

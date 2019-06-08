using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator.StatePattern
{
        public abstract class BaseMonitorState
        {
            protected IMonitorStatable context;

            public BaseMonitorState(IMonitorStatable context)
            {
                this.context = context;
            }

            public abstract void RegisterInput(int number);
        }
    }

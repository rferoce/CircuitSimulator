using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator.StatePattern
{
    public interface IMonitorStatable
    {
        void SetState(BaseMonitorState newState, int number);
    }
}

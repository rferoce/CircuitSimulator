using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator
{
    public interface ISimulatorMediator
    {
        void Notify(Circuit sender, List<Probe> results);
    }
}

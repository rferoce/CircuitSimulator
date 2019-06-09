using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator
{
    public abstract class Simulator : ISimulatorMediator
    {
        public abstract void Notify(Circuit sender, List<Probe> results);

        public abstract void Run();
    }
}

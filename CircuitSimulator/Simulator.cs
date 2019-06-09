using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator
{
    public abstract class Simulator : ISimulatorMediator
    {
        public abstract void AddProbeValuesToEndresult();
        public abstract void AddLastCoutValueToEndResult();
        public abstract void Notify(Circuit sender, List<Probe> results);
        public abstract void Run();

        public virtual void InitializeEndResult()
        {
            // Add S Probe values to end result
            AddProbeValuesToEndresult();

            // Add Cout Probe value of last executed circuit to end result
            AddLastCoutValueToEndResult();
        }
    }
}

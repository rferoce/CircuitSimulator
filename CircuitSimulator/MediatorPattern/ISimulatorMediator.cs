using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator
{
    public interface ISimulatorMediator
    {
        void notify(Node sender, string message);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator.StatePattern
{
    public class MonitoringState : BaseMonitorState
    {
        public MonitoringState(IMonitorStatable context) : base(context) { }

        public override void RegisterInput(int number)
        {
            if (number > 80)
            {
                context.SetState(new WarningState(context), number);
            }
            else
            {
                ConsoleWriterSingleton.Instance.ShowMonitoringStateMessage(number);
            }
        }
    }
}

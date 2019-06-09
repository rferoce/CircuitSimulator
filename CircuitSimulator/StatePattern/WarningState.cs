using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator.StatePattern
{
    public class WarningState : BaseMonitorState
    {
        public WarningState(IMonitorStatable context) : base(context) { }

        public override void RegisterInput(int number)
        {
            if (number <= 80)
            {
                context.SetState(new MonitoringState(context), number);
            }
            else if (number >= 100)
            {
                context.SetState(new ErrorState(context), number);
            } else
            {
                ConsoleWriterSingleton.Instance.ShowWarningStateMessage(number);
            }
        }
    }
}

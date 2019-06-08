using CircuitSimulator.StatePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator
{
    public class App : IMonitorStatable
    {
        private BaseMonitorState _currentState;
        private CircuitSimulator _circuitSimulator;

        public App()
        {
            _currentState = new MonitoringState(this);

            RequestInput();
            CircuitSimulatorBuilder circuitSimulatorBuilder = new CircuitSimulatorBuilder();
            circuitSimulatorBuilder.PrepareCircuitSimulator(new String[] { "C:/Users/renat/OneDrive/Documents/Visual Studio 2015/Projects/Design Patterns 1/CircuitSimulator/CircuitSimulator/Files/Circuit_FullAdder.txt" });
        }

        public void RequestInput()
        {
            ConsoleReader consoleReader = new ConsoleReader();
            string input;
            while ((input = consoleReader.GetInput()) != "exit")
            {
                int number = 0;
                if (Int32.TryParse(input, out number))
                {
                    _currentState.RegisterInput(number);
                } else
                {
                    Console.WriteLine("Could not read the input, please try again");
                }
            }
        }

        public void SetState(BaseMonitorState newState, int number)
        {
            _currentState = newState;
            _currentState.RegisterInput(number);
        }
    }
}
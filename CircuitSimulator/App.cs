using CircuitSimulator.Readers;
using CircuitSimulator.StatePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CircuitSimulator.Input;

namespace CircuitSimulator
{
    public class App : IMonitorStatable
    {
        private ConsoleReader _consoleReader;
        private InputParser _inputParser;
        private BaseMonitorState _currentState;
        private CircuitSimulatorBuilder _circuitSimulatorBuilder;
        private CircuitSimulator _circuitSimulator;
        private List<int> _userInputs;
        private bool _appIsRunning;

        public App()
        {
            _appIsRunning = true;
            while (_appIsRunning)
            {
                // Initialize app
                Initialize();

                // Get input numbers
                GetInputNumbers();

                // Initialize circuitSimulatorBuilder
                InitializeCircuitSimulatorBuilder();

                // Prepare circuitSimulator
                _circuitSimulatorBuilder.PrepareCircuitSimulator(_inputParser.ParseInputs(_userInputs.ToArray()));
                if(_circuitSimulatorBuilder._incorrectSimulator)
                {
                    ConsoleWriterSingleton.Instance.ShowInCorrectFileEnd();
                    _consoleReader.ReadLine();
                    Environment.Exit(0);
                }
                _circuitSimulator = _circuitSimulatorBuilder.GetPreparedCircuitSimulator();

                // Run prepared circuitSimulator
                _circuitSimulator.Run();


                // Ask for another calculation
                AskNewCalculation();
            }
            Environment.Exit(0);
        }

        private void AskNewCalculation()
        {
            ConsoleWriterSingleton.Instance.AskForNewSimulation();

            bool userWantsToExit = !_consoleReader.ReadAnswer();
            if (userWantsToExit)
            {
                _appIsRunning = false;
                ConsoleWriterSingleton.Instance.ThanksForUsage();
                _consoleReader.ReadLine();
            }

            ConsoleWriterSingleton.Instance.ClearConsole();

        }

        private void InitializeCircuitSimulatorBuilder()
        {
            int amountOfCircuits = GetAmountOfCircuits(_userInputs.ToArray());

            string[] circuitPaths = new string[amountOfCircuits];
            for (int i = 0; i < amountOfCircuits; i++)
            {
                circuitPaths[i] = "C:/Users/renat/OneDrive/Documents/Visual Studio 2015/Projects/Design Patterns 1/CircuitSimulator/CircuitSimulator/Files/Circuit_FullAdder.txt";
            }

            _circuitSimulatorBuilder.SetFilePaths(circuitPaths);
        }

        private void GetInputNumbers()
        {
            while (_userInputs.Count() < 2)
            {
                ConsoleWriterSingleton.Instance.AskInput();
                int number = _consoleReader.ReadInput();
                _userInputs.Add(number);
                _currentState.RegisterInput(number);
            }
        }

        private void Initialize()
        {
            _consoleReader = new ConsoleReader();
            _inputParser = new InputParser();
            _userInputs = new List<int>();
            _currentState = new MonitoringState(this);
            _circuitSimulatorBuilder = new CircuitSimulatorBuilder();
            ConsoleWriterSingleton.Instance.ShowStart();
        }

        public int GetAmountOfCircuits(int[] inputs)
        {
            int maxValue = inputs.Max();
            string binary = Convert.ToString(maxValue, 2);
            int amountOfCircuits = binary.Length;
            return amountOfCircuits;
        }

        public void SetState(BaseMonitorState newState, int number)
        {
            _currentState = newState;
            _currentState.RegisterInput(number);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator
{
    public class ConsoleWriterSingleton
    {
        private static ConsoleWriterSingleton _instance;

        public static ConsoleWriterSingleton Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ConsoleWriterSingleton();
                return _instance;
            }
        }

        public static void ShowStart()
        {
            Console.WriteLine();
            Console.WriteLine("     Welcome to this CircuitSimulator!");
            Console.WriteLine("     This simulator is a calculator which can calculate 2 numbers together!");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------");
            Console.WriteLine("     This CircuitSimulator was developed by Gijs Verdonschot and Renato Feroce");
            Console.WriteLine("     With studentnumbers: 2116487, 2108381");
            Console.WriteLine();
            Console.WriteLine("     Enjoy our CircuitSimulator!");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Which 2 numbers from 0 to 1.000.000.000‬ do you want to be calculated togethether by the CircuitSimulator?");
        }

        public void ShowUnReadableInput()
        {
            Console.WriteLine("Could not read the input, please try again");
        }

        public void ShowMonitoringStateMessage(int number)
        {
            Console.WriteLine("Number is normal: {0}", number);
            Console.WriteLine();
            Console.ResetColor();
        }

        public void ShowWarningStateMessage(int number)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("hmm this number is getting really high: {0}", number);
            Console.WriteLine();
            Console.ResetColor();
        }

        public void ShowErrorStateMessage(int number)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Wow this is a high number: {0}, however we should be able to calculate that!", number);
            Console.WriteLine();
            Console.ResetColor();
        }

        public void ShowEmptyLine()
        {
            Console.WriteLine();
        }

        public void AskInput()
        {
            Console.WriteLine("Please enter a whole numerical number (without a comma and numbers after the comma) followed by pressing [Enter]");
        }

        public void AskForNewSimulation()
        {
            Console.WriteLine();
            Console.WriteLine("Do you want to simulate another calculation? (Y/N)");
        }

        public void DrawNode(string node)
        {
            Console.Write(node);
        }

        public void ShowEnglishNodeDescription(string englishDescription, string nodeName)
        {
            Console.Write(englishDescription, nodeName);
        }

        public void ShowNextNode()
        {
            Console.Write("Next Node is: ");
        }

        public void ShowSimulatorEndresult(string binaryResult, string numericalResult)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("INFO - The binary result of this calculation is {0}", binaryResult);
            Console.WriteLine("INFO - The numerical result of this calculation is {0}", numericalResult);
            Console.ResetColor();
        }

        public void ShowInput(string inputName, int inputValue)
        {
            Console.WriteLine();
            Console.WriteLine("Input with name {0}, has value {1} and has next: ", inputName, inputValue);
        }

        public void ThanksForUsage()
        {
            Console.WriteLine("Thanks for using this application!");
            Console.WriteLine("Press any key to exit...");
        }

        public void ClearConsole()
        {
            Console.Clear();
        }

        public void ShowInCorrectFileEnd()
        {
            Console.WriteLine("Press any key to exit");
        }

        public void ShowFirstComponentOfNodeResult()
        {
            Console.WriteLine();
            Console.Write("INFO - ");
        }

        public void ShowMiddleComponentOfNodeResult()
        {
            Console.Write(", has construction ");
        }

        public void ShowResultComponentOfNodeResult(int result)
        {
            Console.Write(", calculated result: {0}", result);
            Console.WriteLine();
        }

        public void ShowNextProbe(string probeName)
        {
            Console.WriteLine("Next node is: {0}", probeName);
            Console.WriteLine();
        }

        public void ShowProbeResult(string probeName, int result)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("INFO - Probe with name {0} ends with result {1}", probeName, result);
            Console.ResetColor();
        }

        public void ShowCircuitResult(string endResult)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("INFO - The result of this circuit calculation is {0}", endResult);
            Console.ResetColor();
        }

        public void ShowUnLinkedNodeMessage(string nodeName)
        {
            Console.WriteLine("ERROR: Node with name {0} is not linked to next Node!", nodeName);
        }

        public void ShowInfiniteLoopMessage(string nodeName)
        {
            Console.WriteLine("ERROR: Found infinite loop. Node with name {0} was called multiple times!", nodeName);
        }
    }
}

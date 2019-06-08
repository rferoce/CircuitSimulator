using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator
{
    public abstract class Simulator : ISimulatorMediator
    {
        public virtual void getDiscription()
        {
            Console.WriteLine("A simulator simulates it's children!");
        }

        public void notify(Node sender, string message)
        {
            throw new NotImplementedException();
        }

        public void Notify(Node sender, string message)
        {
            throw new NotImplementedException();
        }

        public abstract void Initialize();
        public abstract void Run();

        public void Simulate()
        {
            Console.WriteLine("Simulator is going to be initialized...");
            Initialize();

            Console.WriteLine("Simulator is going to be executed...");
            Run();

        }
    }
}

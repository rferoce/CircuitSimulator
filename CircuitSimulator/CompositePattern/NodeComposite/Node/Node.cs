using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CircuitSimulator.Input;

namespace CircuitSimulator
{
    public abstract class Node : INodeComponent
    {
        public ICircuitMediator _circuitMediator { get; set; }
        public List<InputValue> _inputs = new List<InputValue>();
        private INodeVisitor _nodeEnglishDescriptionVisitor = new NodeEnglishDescriptionVisitor();
        private INodeComponent _parent;
        public string _name;
        public int InputsAmount { get; set; }

        public void SetCircuitMediator(ICircuitMediator circuitMediator)
        {
            _circuitMediator = circuitMediator;
        }

        public List<INodeComponent> Children
        {
            get
            {
                return new List<INodeComponent>();
            }
        }

        public INodeComponent Parent
        {
            get
            {
                return _parent;
            }

            set
            {
                _parent = value;
            }
        }

        public abstract Node Clone();

        public virtual void AddInput(InputValue input)
        {
            _inputs.Add(input);
            if (_inputs.Count() == InputsAmount)
            {
                this.Calculate();
            }
        }

        public abstract void Calculate();

        public bool CanCalculate()
        {
            return true;
        }

        public void ChildrenToString()
        {
            foreach (var child in Parent.Children)
            {
                if (!child.CanCalculate())
                {
                    FindNextNodes(child);
                }
            }
        }

        public void FindNextNodes(INodeComponent nodeComposite)
        {
            bool foundNodeChild = false;

            foreach (var child in nodeComposite.Children)
            {
                if (child.CanCalculate())
                {
                    ConsoleWriterSingleton.Instance.ShowNextNode();
                    child.Accept(_nodeEnglishDescriptionVisitor);
                    ConsoleWriterSingleton.Instance.ShowEmptyLine();
                    foundNodeChild = true;
                }
            }

            if (!foundNodeChild)
            {
                foreach (var child in nodeComposite.Children)
                {
                    FindNextNodes(child);
                }
            }
        }

        public abstract void Accept(INodeVisitor visitor);
    }
}

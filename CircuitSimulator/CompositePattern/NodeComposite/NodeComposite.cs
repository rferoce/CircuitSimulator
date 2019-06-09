using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CircuitSimulator.Input;

namespace CircuitSimulator
{
    public class NodeComposite : INodeComponent
    {
        private List<INodeComponent> _children = new List<INodeComponent>();
        private INodeVisitor _nodeEnglishDescriptionVisitor = new NodeEnglishDescriptionVisitor();
        private INodeComponent _parent;
        public void AddChild(INodeComponent child)
        {
            child.Parent = this;
            _children.Add(child);
        }

        public List<INodeComponent> Children
        {
            get
            {
                return _children;
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

        public bool CanCalculate()
        {
            return false;
        }

        public void AddInput(InputValue input)
        {
        }

        public void Accept(INodeVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}

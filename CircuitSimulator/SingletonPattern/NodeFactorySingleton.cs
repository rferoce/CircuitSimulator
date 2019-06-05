using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator
{
    public class NodeFactorySingleton
    {
        private Dictionary<string, Type> _types;
        private static NodeFactorySingleton _instance;

        public static NodeFactorySingleton Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new NodeFactorySingleton();
                return _instance;
            }
        }

        public void addNodeType(string name, Type type)
        {
            _types[name] = type;
        }

        public Node CreateNode(string type)
        {
            Type t = _types[type];
            Node c = (Node)Activator.CreateInstance(t);
            return c;        }
    }
}

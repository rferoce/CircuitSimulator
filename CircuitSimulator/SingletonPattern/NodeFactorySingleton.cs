using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator
{
    public class NodeFactorySingleton
    {
        private Dictionary<string, Node> _types = new Dictionary<string, Node>();
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

        public void RegisterNode(string name, Node prototype)
        {
            _types[name] = prototype;
        }

        public Node CreateNode(string type)
        {
            Node prototype = _types[type];
            return prototype.Clone();        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CircuitSimulator
{
    public class StringParser
    {
        private Dictionary<string, string[]> _componentMapping;
        private Dictionary<string, string[]> _edgeMapping;

        public Dictionary<string, string[]>[] readFileString(string fileString)
        {
            // Filter file 
            Regex rgx = new Regex("[^a-zA-Z0-9,_;:]");
            fileString = rgx.Replace(fileString, "");

            // Create mappings
            CreateMappings(fileString);

            return new Dictionary<string, string[]>[] { _componentMapping, _edgeMapping };
        }

        private void CreateMappings(string fileString)
        {
            string[] lines = fileString.Split(';');

            _componentMapping = new Dictionary<string, string[]>();
            _edgeMapping = new Dictionary<string, string[]>();

            // Loop through each file and add KeyValuePairs to correct mappings
            foreach (string line in lines)
            {
                if (line.Length > 0)
                {
                    string[] stringComponents = line.Split(':');
                    string key = stringComponents[0];
                    string[] values = stringComponents[1].Split(',');

                    if (_componentMapping.ContainsKey(key))
                    {
                        _edgeMapping.Add(key, values);
                    }
                    else
                    {
                        _componentMapping.Add(key, values);
                    }
                }
            }
        }
    }
}

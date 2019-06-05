using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator
{
    public class StringParser
    {
        public Dictionary<string, string[]> readFileString(string fileString)
        {
            string[] lines = fileString.Trim().Split(';');
            Dictionary<string, string[]> mapping = new Dictionary<string, string[]>();

            foreach (var line in lines)
            {
                string[] stringComponents = line.Split(':');
                string key = stringComponents[0];
                string[] values = stringComponents[1].Split(',');

                mapping.Add(key, values);
            }

            return mapping;
        }
    }
}

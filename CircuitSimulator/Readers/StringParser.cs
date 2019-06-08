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
        public Dictionary<string, string[]>[] readFileString(string fileString)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9,_;:]");
            fileString = rgx.Replace(fileString, "");

            string[] lines = fileString.Split(';');

            Dictionary<string, string[]> componentMapping = new Dictionary<string, string[]>();
            Dictionary<string, string[]> edgeMapping = new Dictionary<string, string[]>();


            foreach (var line in lines)
            {
                if (line.Length > 0)
                {
                    string[] stringComponents = line.Split(':');
                    string key = stringComponents[0];
                    string[] values = stringComponents[1].Split(',');

                    if (componentMapping.ContainsKey(key))
                    {
                        edgeMapping.Add(key, values);
                    }
                    else
                    {
                        componentMapping.Add(key, values);
                    }
                }
            }

            return new Dictionary<string, string[]>[] { componentMapping, edgeMapping };
        }
    }
}

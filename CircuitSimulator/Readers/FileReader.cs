using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator
{
    public class FileReader
    {
        private StringParser _stringParser;
        public string ReadFile(string filePath)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string fileString = sr.ReadToEnd();
                    return fileString;
                }
            } catch (IOException e)
            {
                return null;
            }
        }
    }
}

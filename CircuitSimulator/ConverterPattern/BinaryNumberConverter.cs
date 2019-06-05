using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator
{
    public class BinaryNumberConverter : INumberConverter
    {
        public string ToLocalString(int number)
        {
            return Convert.ToString(number, 2);
        }

        public int ToNumerical(string fromText)
        {
            try
            {
                // TODO: set AcceptedInputState!
                return Convert.ToInt32(fromText, 2);
            }
            catch (FormatException e)
            {
                // TODO: set ErrorInputState!
                return 0;
            }
        }
    }
}

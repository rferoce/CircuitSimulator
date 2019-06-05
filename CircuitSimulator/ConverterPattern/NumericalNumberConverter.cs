using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSimulator
{
    public class NumericalNumberConverter : INumberConverter
    {
        public string ToLocalString(int number)
        {
            return number.ToString();
        }

        public int ToNumerical(string fromText)
        {
            try
            {
                // TODO: Set AcceptedInputState!
                return Int32.Parse(fromText);
            }
            catch (FormatException e)
            {
                // TODO: Set ErrorInputState!
                return 0;
            }
        }
    }
}

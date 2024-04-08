using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApp.Classes
{
    public class ByteHelper
    {
        public static int Compare(byte[] x, byte[] y)
        {
            for (var i = 0; i < Math.Min(x.Length, y.Length); i++)
                if (x[i] != y[i])
                    return x[i] - y[i];

            return x.Length - y.Length;
        }
    }
}

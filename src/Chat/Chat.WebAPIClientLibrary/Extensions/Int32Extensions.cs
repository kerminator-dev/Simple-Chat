using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.WebAPIClientLibrary.Extensions
{
    public static class Int32Extensions
    {
        public static bool Between(this int num, int lower, int upper, bool inclusive = true)
        {
            return inclusive
                ? lower <= num && num <= upper
                : lower < num && num < upper;
        }
    }
}

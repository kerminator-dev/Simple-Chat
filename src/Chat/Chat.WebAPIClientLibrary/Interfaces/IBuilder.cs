using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.WebAPIClientLibrary.Interfaces
{
    internal interface IBuilder<T>
    {
        T Build();
    }
}

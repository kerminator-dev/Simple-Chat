using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.WebAPIClientLibrary.Exceptions.Client
{
    public class SignInException : Exception
    {
        public SignInException(string message) : base(message) { }
    }
}

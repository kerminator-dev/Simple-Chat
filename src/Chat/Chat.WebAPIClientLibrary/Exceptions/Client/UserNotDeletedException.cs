using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.WebAPIClientLibrary.Exceptions.Client
{
    public class UserNotDeletedException : Exception
    {
        public UserNotDeletedException(string message) : base(message) { }
    }
}

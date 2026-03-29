using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException() : base(BllMessages.InvalidPassword) { }
    }
}

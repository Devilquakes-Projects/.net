using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Exceptions
{
    class InvalidPasswordException : ApplicationException
    {
        public InvalidPasswordException() : base("Invalid Password.") { }
    }
}

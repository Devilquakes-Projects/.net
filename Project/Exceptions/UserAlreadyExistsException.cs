using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Exceptions
{
    class UserAlreadyExistsException : ApplicationException
    {
        public UserAlreadyExistsException() : base("User already exists.") { }
    }
}

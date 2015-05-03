using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Exceptions
{
    class InvalidTeacherCodeException : ApplicationException
    {
        public InvalidTeacherCodeException() : base("Invalid Teacher Code.") { }
    }
}

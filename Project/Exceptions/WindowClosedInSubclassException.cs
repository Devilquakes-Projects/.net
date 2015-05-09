using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Exceptions
{
    class WindowClosedInSubclassException : ApplicationException
    {
        public WindowClosedInSubclassException() : base("Window was: Closed in subclass") { }
    }
}

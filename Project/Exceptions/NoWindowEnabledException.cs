using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Exceptions
{
    class NoWindowEnabledException : ApplicationException
    {
        public NoWindowEnabledException() : base("There is no window enabled at this time") { }
    }
}

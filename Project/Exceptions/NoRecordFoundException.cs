using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Exceptions
{
    class NoRecordFoundException : ApplicationException
    {
        public NoRecordFoundException() : base("No Record Found.") { }
    }
}

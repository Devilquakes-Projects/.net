using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Exceptions
{
    class CourseAlreadyCompletedException : ApplicationException
    {
        public CourseAlreadyCompletedException() : base("You already completed this Curriculum") { }
    }
}

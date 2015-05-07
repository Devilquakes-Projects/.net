using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Author: Greg, Date: 07-05-15 12:00 - 12:30
namespace Project.Exceptions
{
    class CourseAlreadyCompletedException : ApplicationException
    {
        public CourseAlreadyCompletedException() : base("You already completed this Curriculum") { }
    }
}

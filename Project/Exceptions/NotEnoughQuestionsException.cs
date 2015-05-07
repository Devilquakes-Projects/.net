using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Author: Greg, Date: 07-05-15 12:00 - 13:00
namespace Project.Exceptions
{
    class NotEnoughQuestionsException : ApplicationException
    {
        public NotEnoughQuestionsException() : base("There are not enough questions in the question list, ask a teacher to fix this.")
        {
        }
    }
}

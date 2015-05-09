using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

// Auther: Gregory Malomgré
// Date: 29/04/2015 16:00-16:30
namespace Project.Controllers.Courses
{
    public abstract class Courses_TextBoxClass : Curriculum
    {

        public Courses_TextBoxClass(int studentId, int difficulty)
            : base(studentId, difficulty)
        { }//create a top-class object, so do nothing here

        protected void LockTextBoxAndIsAnswerCorrect(TextBox tb, bool isCorrect)
        {
            tb.IsReadOnly = true;

            if (isCorrect)
            {
                tb.Background = Brushes.LightGreen;
            }
            else
            {
                tb.Background = Brushes.LightPink;
            }
        }
    }
}

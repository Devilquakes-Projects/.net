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

        public void SetTextboxStartSize(double tbWidth = 200, double tbMaxWidth = 1.25, int amountOfCharactersAllowed = 20, TextBox tb1 = null, TextBox tb2 = null, TextBox tb3 = null, TextBox tb4 = null, TextBox tb5 = null, TextBox tb6 = null)//Author: Greg, Date: 23/04/15 16:30-18:30
        {
            TextBox[] tb = { tb1, tb2, tb3, tb4, tb5, tb6 };//Gewijzigd door Joren Martens, Date:???
            for (int i = 0; i < tb.Length && tb[i] != null; i++)//Gewijzigd op 29/04 door Gregory
            {
                tb[i].MinWidth = tbWidth;
                tb[i].MaxWidth = tbWidth * tbMaxWidth;
                tb[i].MaxLength = amountOfCharactersAllowed;
            }
        }

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

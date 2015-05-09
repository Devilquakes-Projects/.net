// Author: Joris Meylaers
// Date: 14/04/2015

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.Controllers
{
    public class Input
    {
        /// <summary>
        /// Load list of available Keyboard buttons
        /// </summary>
        private static Hashtable keyTable = new Hashtable();

        /// <summary>
        /// Perform a check to see if a particular button is pressed.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool KeyPressed(Key key)
        {
            if (keyTable[key] == null)
            {
                return false;
            }
            return (bool)keyTable[key];
        }

        /// <summary>
        /// Detect if a keyboard button is pressed
        /// </summary>
        /// <param name="key"></param>
        /// <param name="state"></param>
        public static void ChangeState(Key key, bool state)
        {
            keyTable[key] = state;
        }
    }
}

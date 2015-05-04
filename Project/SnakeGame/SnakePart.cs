// Auther: Joris Meylaers
// Date: 01/04/2015

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.SnakeGame
{
    class SnakePart
    {
        /// <summary>
        /// Sets default X and Y values on 0
        /// </summary>
        public SnakePart()
        {
            X = 0;
            Y = 0;
        }

        /// <summary>
        /// get or set the X value of a SnakePart
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// get or set the X value of a SnakePart
        /// </summary>
        public int Y { get; set; }
    }
}

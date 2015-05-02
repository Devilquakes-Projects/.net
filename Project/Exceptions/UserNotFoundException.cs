﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Exceptions
{
    class UserNotFoundException : ApplicationException
    {
        public UserNotFoundException() : base("User not found.") { }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Student : BaseUser
    {
        public Grade Grade { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGR_Futbal.Model
{
    public class Out : Udalost
    {
        public int IdOut { get; set; }
        public int IdUdalost { get; set; }
        public Hrac Hrac { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzTravel
{
    [Serializable]
    public class Config
    {

        public bool Mode = false;
        public TravelPoint[] TravelPoints;
    }
}

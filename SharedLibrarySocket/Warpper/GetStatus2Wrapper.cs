﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrarySocket.Warpper
{
    [Serializable]
    public class GetStatus2Wrapper
    {
        public MotorStatus Motor1;
        public MotorStatus Motor2;
    }
}

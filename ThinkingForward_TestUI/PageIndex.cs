﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThinkingForward_TestUI
{
    public class PageIndex
    {
        public PageIndex(int index, bool isActive)
        {
            Index = index;
            IsActive = isActive;
        }

        public int Index { get; }

        public bool IsActive { get; }
    }
}

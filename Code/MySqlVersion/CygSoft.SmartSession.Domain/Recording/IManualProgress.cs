﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Recording
{
    public interface IManualProgress
    {
        int Weighting { get; }
        int CalculateProgress();

        IManualProgress Increase(int value);
        IManualProgress Decrease(int value);

        IManualProgress NewManualProgress(int value);
    }
}

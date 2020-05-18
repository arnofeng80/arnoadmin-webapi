﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.Base.Search
{
    public enum Operation
    {
        Equals,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual,
        Contains,
        StartsWith,
        EndsWith
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SJ.Core;

namespace SJ.Web
{
    public interface IJobDetailView : IView
    {
        void SetJob(int jobId);
    }
}

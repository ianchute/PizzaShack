﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Data
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
    }
}
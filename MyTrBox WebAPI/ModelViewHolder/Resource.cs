﻿using MyTrBox_WebAPI.ModelViewHolder;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.ModelViewHolder
{
    public abstract class Resource : Link
    {
        [JsonIgnore]
        public Link Self { get; set; }
    }
}

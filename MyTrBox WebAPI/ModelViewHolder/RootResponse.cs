using MyTrBox_WebAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.ModelViewHolder
{
    public class RootResponse : Resource
    {
        public Link Artist { get; set; }
        public Link Category { get; set; }
        public Link Song { get; set; }
    }
}

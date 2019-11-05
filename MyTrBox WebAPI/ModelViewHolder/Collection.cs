using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.ModelViewHolder
{
    public class Collection<T> : Resource
    {
        public T[] Value { set; get; }
    }
}

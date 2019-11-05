using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.ModelViewHolder
{
    public class PagedResult<T>
{
        public IEnumerable<T> Items { get; set; }
        public int TotalSize { get; set; }
}
}

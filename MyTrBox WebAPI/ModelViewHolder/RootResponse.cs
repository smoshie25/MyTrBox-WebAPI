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
        public Link Genre { get; set; }
        public Link Song { get; set; }
        public Link SongAlbum { get; set; }
        public Link VideoAlbum { get; set; }
        public Link Video { get; set; }
    }
}

using MyTrBox_WebAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.ModelViewHolder
{
    public class ArtistView : Resource
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Profile { get; set; }

        public Genre Genre { get; set; }
        public ICollection<Song> Songs { get; set; }

    }
}

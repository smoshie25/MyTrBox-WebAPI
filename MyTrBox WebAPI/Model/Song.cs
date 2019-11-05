using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.Model
{
    public class Song
{
        public Guid SongId { get; set; }
        public Guid GenreId { get; set; }
        public Guid ArtistId { get; set; }
        public string Title { get; set; }
        public Artist Artist { get; set; }
    }
}

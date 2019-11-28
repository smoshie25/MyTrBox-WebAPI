using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.Model
{
    public class Song
{
        public Guid SongId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid ArtistId { get; set; }
        public string Title { get; set; }
        public string Media { get; set; }
        public string Image { get; set; }
        public Artist Artist { get; set; }
    }
}

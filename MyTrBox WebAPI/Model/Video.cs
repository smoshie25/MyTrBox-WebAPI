using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.Model
{
    public class Video
{
        public Guid Id { get; set; }
        public Guid GenreId { get; set; }
        public Guid ArtistId { get; set; }
        public Guid AlbumId { get; set; }
        public string Title { get; set; }
        public string Media { get; set; }
        public string Image { get; set; }
        public Artist Artist { get; set; }
        public Genre Genre { get; set; }
        public SongAlbum Album { get; set; }
    }
}

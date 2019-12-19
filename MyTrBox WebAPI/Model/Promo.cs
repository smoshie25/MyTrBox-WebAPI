using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.Model
{
    public class Promo
{
        public Guid Id { get; set; }
        public Guid SongAlbumId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public SongAlbum SongAlbum { get; set; }
    }
}

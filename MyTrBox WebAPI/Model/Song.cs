using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.Model
{
    public class Song
{
        public Guid Id { get; set; }
        public Guid GenreId { get; set; }
        public Guid ArtistId { get; set; }
        public Guid AlbumId { get; set; }
        public string Title { get; set; }
        public string Media { get; set; }
        public string Image { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Artist Artist { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Genre Genre { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public SongAlbum Album { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.Model
{
    public class SongAlbum
    {
        public Guid Id { get; set; }
        public Guid ArtistId { get; set; }
        public string Name { get; set; }
        public Artist Artist { get; set; }
        public string Description { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<Song> Songs { get; set; }
    }
}

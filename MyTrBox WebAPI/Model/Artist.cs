using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.Model
{
    public class Artist
    {
        public Guid id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Image { get; set; }
        public string Profile { get; set; }

        // Foreign Key
        public Guid CategoryId { get; set; }
        // Navigation property
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Category Category { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<SongAlbum> SongAlbums { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<VideoAlbum> VideoAlbums { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<Song> Songs { get; set; }
    }
}

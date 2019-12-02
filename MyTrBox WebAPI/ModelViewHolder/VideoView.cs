using MyTrBox_WebAPI.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.ModelViewHolder
{
    public class VideoView : Resource
{
        public Guid VideoId { get; set; }
        public string Title { get; set; }
        public string Media { get; set; }
        public string Image { get; set; }
        public Form Video { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Artist Artist { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public GenreView Genre { get; set; }
    }
}

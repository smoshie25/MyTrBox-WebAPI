using MyTrBox_WebAPI.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.ModelViewHolder
{
    public class SongView : Resource
{
        public Guid SongId { get; set; }
        public string Title { get; set; }
        public string Media { get; set; }
        public string Image { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Artist Artist { get; set; }
    }
}

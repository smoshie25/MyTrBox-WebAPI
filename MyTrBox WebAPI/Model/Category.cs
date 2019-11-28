using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.Model
{
    public class Category
{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<Artist> Artists { get; set; }
    }
}

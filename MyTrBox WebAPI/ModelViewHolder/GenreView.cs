using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.ModelViewHolder
{
    public class GenreView : Resource
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

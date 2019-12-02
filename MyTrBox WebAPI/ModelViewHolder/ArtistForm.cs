using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.ModelViewHolder
{
    public class ArtistForm
{
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        public string Profile { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}

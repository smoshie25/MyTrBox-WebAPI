using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.ModelViewHolder
{
    public class SongForm
{
        [Required]
        public string Title { get; set; }
        [Required]
        public Guid ArtistId { get; set; }
        public IFormFile Media { get; set; }
        public IFormFile Image { get; set; }
    }
}

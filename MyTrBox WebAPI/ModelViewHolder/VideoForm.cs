using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.ModelViewHolder
{
    public class VideoForm
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public Guid ArtistId { get; set; }
        [Required]
        public Guid GenreId { get; set; }
        [Required]
        public Guid AlbumId { get; set; }
        [Required]
        public IFormFile Media { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}

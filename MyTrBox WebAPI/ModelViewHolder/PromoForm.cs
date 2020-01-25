using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using MyTrBox_WebAPI.Model;

namespace MyTrBox_WebAPI.ModelViewHolder
{
    public class PromoForm
    {
        public Guid TypeId { get; set; }
        public string Name { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        public PromoType PromoType { get; set; }
    }
}

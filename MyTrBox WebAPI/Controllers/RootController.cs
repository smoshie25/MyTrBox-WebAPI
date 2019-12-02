using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyTrBox_WebAPI.ModelViewHolder;

namespace MyTrBox_WebAPI.Controllers
{
    [Route("/")]
    [ApiController] 
    public class RootController : ControllerBase
    {
        
        [HttpGet(Name =nameof(GetRoot))]
        public IActionResult GetRoot()
        {
            var response = new RootResponse
            {
                Self = Link.To(nameof(GetRoot)),
                Category = Link.ToCollection(nameof(CategoryController.GetCategory)),
                Genre = Link.ToCollection(nameof(GenreController.GetGenre)),
                Artist = Link.ToCollection(nameof(ArtistController.GetArtist)),
                Song = Link.ToCollection(nameof(SongController.GetSong)),
                Video = Link.ToCollection(nameof(VideoController.GetVideo)),
                
            };

            return Ok(response);
        }
    }
}
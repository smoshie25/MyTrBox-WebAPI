using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyTrBox_WebAPI.Interfaces;
using MyTrBox_WebAPI.Model;
using MyTrBox_WebAPI.ModelViewHolder;

namespace MyTrBox_WebAPI.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly ISong _iSong;
        private readonly PagingOptions _defaultPagingOptions;

        public SongController(ISong iSong, IOptions<PagingOptions> defaultPagingOptions)
        {
            _iSong = iSong;
            _defaultPagingOptions = defaultPagingOptions.Value;
        }

        [HttpGet(Name = nameof(GetSong))]
        public async Task<ActionResult<Collection<SongView>>> GetSong
            ([FromQuery] PagingOptions pagingOptions = null)
        {
            pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;
            pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;

            var songs = await _iSong.GetAllSongAsync(pagingOptions);

            var collection = PagedCollection<SongView>.Create(Link.ToCollection(nameof(GetSong)),
                songs.Items.ToArray(),
                songs.TotalSize,
                pagingOptions,null
                ); 

            return collection;
        }


        [HttpGet("{songID}",Name = nameof(GetSongById))]
        public async Task<ActionResult<SongView>> GetSongById(Guid songID)
        {
            var song = await _iSong.GetSong(songID);

            if (song == null)
            {
                return NotFound();
            }
            else {
                return song;
            }

        }

        [HttpPost(Name = nameof(SaveSong))]
        public async Task<ActionResult> SaveSong([FromForm] SongForm songForm) 
        {
            var songId = await _iSong.SaveSong(songForm);

            return Created(Url.Link(nameof(CategoryController.GetCategory), new
            {
                songId
            }), songId);
        }
    }
}
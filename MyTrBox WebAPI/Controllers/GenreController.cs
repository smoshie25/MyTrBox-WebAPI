using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyTrBox_WebAPI.Interfaces;
using MyTrBox_WebAPI.Model;
using MyTrBox_WebAPI.ModelViewHolder;

namespace MyTrBox_WebAPI.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenre _genre;
        private readonly PagingOptions _defaultPagingOptions;

        public GenreController(IGenre genre, IOptions<PagingOptions> defaultPagingOptions) {
            _genre = genre;
            _defaultPagingOptions = defaultPagingOptions.Value;
        }

        // GET: Genre
        [HttpGet(Name = nameof(GetGenre))]
        public async Task<ActionResult<Collection<GenreView>>> GetGenre()
        {
            var genres = await _genre.GetAllGenreAsync();

            var collection = new Collection<GenreView>
            {
                Self = Link.ToCollection(nameof(GetGenre)),
                Value = genres.ToArray()
            };

            return collection;
        }

        // GET: Genre/5
        [HttpGet("{Id}",Name = nameof(GetGenreSong))]
        public async Task<ActionResult<Collection<SongView>>> GetGenreSong( Guid Id, [FromQuery] PagingOptions pagingOptions = null)
        {
            pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;
            pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;

            var Songs = await _genre.GetSongsByGenreAsync(Id,pagingOptions);

            var collection = PagedCollection<SongView>.Create(Link.ToCollection(nameof(GetGenreSong)),
                Songs.Items.ToArray(),
                Songs.TotalSize,
                pagingOptions,null
                );

            return collection;
        }

        // POST: api/Genre
        [HttpPost(Name = nameof(Post))]
        public async Task<ActionResult> Post([FromBody] GenreForm genre)
        {
            var genreId = await _genre.SaveGenre(genre);

            return Created(Url.Link(nameof(GenreController.GetGenre), new {
                genreId
            }) , new { Id = genreId });
        }

        // PUT: api/Genre/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

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
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _genre;
        private readonly PagingOptions _defaultPagingOptions;

        public CategoryController(ICategory genre, IOptions<PagingOptions> defaultPagingOptions) {
            _genre = genre;
            _defaultPagingOptions = defaultPagingOptions.Value;
        }

        // GET: Genre
        [HttpGet(Name = nameof(GetGenre))]
        public async Task<ActionResult<Collection<CategoryView>>> GetGenre()
        {
            var genres = await _genre.GetAllCategoryAsync();

            var collection = new Collection<CategoryView>
            {
                Self = Link.ToCollection(nameof(GetGenre)),
                Value = genres.ToArray()
            };

            return collection;
        }

        // GET: Genre/5
        [HttpGet("{Id}",Name = nameof(GetGenreArtist))]
        public async Task<ActionResult<Collection<ArtistView>>> GetGenreArtist( Guid Id, [FromQuery] PagingOptions pagingOptions = null)
        {
            pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;
            pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;

            var artists = await _genre.GetArtistByCategoryAsync(Id,pagingOptions);

            var collection = PagedCollection<ArtistView>.Create(Link.ToCollection(nameof(GetGenreArtist)),
                artists.Items.ToArray(),
                artists.TotalSize,
                pagingOptions
                );

            return collection;
        }

        // POST: api/Genre
        [HttpPost(Name = nameof(Post))]
        public async Task<ActionResult> Post([FromBody] CategoryForm genre)
        {
            var genreId = await _genre.SaveCategory(genre);

            return Created(Url.Link(nameof(CategoryController.GetGenre), new {
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

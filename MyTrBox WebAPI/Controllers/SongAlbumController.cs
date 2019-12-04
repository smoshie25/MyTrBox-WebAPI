using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyTrBox_WebAPI.Infrastructure;
using MyTrBox_WebAPI.Interfaces;
using MyTrBox_WebAPI.Model;
using MyTrBox_WebAPI.ModelViewHolder;

namespace MyTrBox_WebAPI.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class SongAlbumController : ControllerBase
    {
        private readonly ISongAlbum _album;
        private readonly PagingOptions _defaultPagingOptions;

        public SongAlbumController(ISongAlbum album, IOptions<PagingOptions> defaultPagingOptions) {
            _album = album;
            _defaultPagingOptions = defaultPagingOptions.Value;
        }

        // GET: Genre
        [HttpGet(Name = nameof(GetAllSongAlbum))]
        public async Task<ActionResult<Collection<SongAlbumView>>> GetAllSongAlbum()
        {
            var album = await _album.GetAllSongAlbumAsync();

            var collection = new Collection<SongAlbumView>
            {
                Self = Link.ToCollection(nameof(GetSongAlbum)),
                Form = FormMetadata.FromModel(
                        new AlbumForm(),
                        Link.ToForm(
                            nameof(SongAlbumController.PostSongAlbum),
                            null,
                            Link.PostMethod,
                            Form.CreateRelation)),
                Value = album.ToArray()
                
            };
            return collection;
        }

        // GET: Genre/5
        [HttpGet("{Id}",Name = nameof(GetSongAlbum))]
        public async Task<ActionResult<Collection<SongView>>> GetSongAlbum( Guid Id, [FromQuery] PagingOptions pagingOptions = null)
        {
            pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;
            pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;

            var artists = await _album.GetSongByAlbumAsync(Id,pagingOptions);

            var collection = PagedCollection<SongView>.Create(Link.ToCollection(nameof(GetSongAlbum)),
                artists.Items.ToArray(),
                artists.TotalSize,
                pagingOptions,null
                );

            return collection;
        }

        // POST: api/Genre
        [HttpPost(Name = nameof(PostSongAlbum))]
        public async Task<ActionResult> PostSongAlbum([FromBody] AlbumForm genre)
        {
            var albumId = await _album.SaveSongAlbum(genre);

            return Created(Url.Link(nameof(GetSongAlbum), new {
                albumId
            }) , new { Id = albumId });
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

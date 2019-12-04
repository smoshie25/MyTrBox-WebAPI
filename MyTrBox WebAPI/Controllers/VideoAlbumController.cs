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
    public class VideoAlbumController : ControllerBase
    {
        private readonly IVideoAlbum _album;
        private readonly PagingOptions _defaultPagingOptions;

        public VideoAlbumController(IVideoAlbum album, IOptions<PagingOptions> defaultPagingOptions) {
            _album = album;
            _defaultPagingOptions = defaultPagingOptions.Value;
        }

        // GET: Genre
        [HttpGet(Name = nameof(GetAllVideoAlbum))]
        public async Task<ActionResult<Collection<VideoAlbumView>>> GetAllVideoAlbum()
        {
            var album = await _album.GetAllVideoAlbumAsync();

            var collection = new Collection<VideoAlbumView>
            {
                Self = Link.ToCollection(nameof(GetVideoAlbum)),
                Form = FormMetadata.FromModel(
                        new AlbumForm(),
                        Link.ToForm(
                            nameof(VideoAlbumController.PostVideoAlbum),
                            null,
                            Link.PostMethod,
                            Form.CreateRelation)),
                Value = album.ToArray()
                
            };
            return collection;
        }

        // GET: Genre/5
        [HttpGet("{Id}",Name = nameof(GetVideoAlbum))]
        public async Task<ActionResult<Collection<VideoView>>> GetVideoAlbum( Guid Id, [FromQuery] PagingOptions pagingOptions = null)
        {
            pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;
            pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;

            var artists = await _album.GetVideoByAlbumAsync(Id,pagingOptions);

            var collection = PagedCollection<VideoView>.Create(Link.ToCollection(nameof(GetVideoAlbum)),
                artists.Items.ToArray(),
                artists.TotalSize,
                pagingOptions,null
                );

            return collection;
        }

        // POST: api/Genre
        [HttpPost(Name = nameof(PostVideoAlbum))]
        public async Task<ActionResult> PostVideoAlbum([FromBody] AlbumForm albumForm)
        {
            var albumId = await _album.SaveVideoAlbum(albumForm);

            return Created(Url.Link(nameof(GetVideoAlbum), new {
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class ArtistController : ControllerBase
    {
        private readonly IArtist _iArtist;
        private readonly PagingOptions _defaultPagingOptions;

        public ArtistController(IArtist artist, IOptions<PagingOptions> defaultPagingOptions)
        {
            _iArtist = artist;
            _defaultPagingOptions = defaultPagingOptions.Value;
        }

        [HttpGet(Name = nameof(GetArtist))]
        public async Task<ActionResult<Collection<ArtistView>>> GetArtist
            ([FromQuery] PagingOptions pagingOptions = null)
        {
            pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;
            pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;

            var artists = await _iArtist.GetAllArtistAsync(pagingOptions);

            var collection = PagedCollection<ArtistView>.Create(Link.ToCollection(nameof(GetArtist)),
                artists.Items.ToArray(),
                artists.TotalSize,
                pagingOptions,
                FormMetadata.FromModel(
                        new ArtistForm(),
                        Link.ToForm(
                            nameof(ArtistController.SaveArtist),
                            null,
                            Link.PostMethod,
                            Form.CreateRelation))
                ); 

            return collection;
        }


        // GET: Genre/5
        [HttpGet("{Id}", Name = nameof(GetArtistSongs))]
        public async Task<ActionResult<Collection<SongView>>> GetArtistSongs(Guid Id, [FromQuery] PagingOptions pagingOptions = null)
        {
            pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;
            pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;

            var artists = await _iArtist.GetSongsByArtistAsync(Id, pagingOptions);

            var collection = PagedCollection<SongView>.Create(Link.ToCollection(nameof(GetArtistSongs)),
                artists.Items.ToArray(),
                artists.TotalSize,
                pagingOptions,null
                );

            return collection;
        }

        [HttpPost(Name = nameof(SaveArtist))]
        public async Task<ActionResult> SaveArtist([FromForm] ArtistForm artist) 
        {
            var artistId = await _iArtist.SaveArtist(artist);

            return Created(Url.Link(nameof(CategoryController.GetCategory), new
            {
                artistId
            }), artistId);
        }
    }
}
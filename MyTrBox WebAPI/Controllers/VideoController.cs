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
    public class VideoController : ControllerBase
    {
        private readonly IVideo _iVideo;
        private readonly PagingOptions _defaultPagingOptions;

        public VideoController(IVideo iVideo, IOptions<PagingOptions> defaultPagingOptions)
        {
            _iVideo = iVideo;
            _defaultPagingOptions = defaultPagingOptions.Value;
        }

        [HttpGet(Name = nameof(GetVideo))]
        public async Task<ActionResult<Collection<VideoView>>> GetVideo
            ([FromQuery] PagingOptions pagingOptions = null)
        {
            pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;
            pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;

            var Videos = await _iVideo.GetAllVideoAsync(pagingOptions);

            var collection = PagedCollection<VideoView>.Create(Link.ToCollection(nameof(GetVideo)),
                Videos.Items.ToArray(),
                Videos.TotalSize,
                pagingOptions,
                FormMetadata.FromModel(
                        new VideoForm(),
                        Link.ToForm(
                            nameof(VideoController.SaveVideo),
                            null,
                            Link.PostMethod,
                            Form.CreateRelation))
                ); 

            return collection;
        }


        [HttpGet("{artistId}", Name = nameof(GetVideoById))]
        public async Task<ActionResult<Collection<VideoView>>> GetVideoById(Guid artistId,
            [FromQuery] PagingOptions pagingOptions = null)
        {
            pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;
            pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;


            var Videos = await _iVideo.GetVideoByArtist(artistId,pagingOptions);

            var collection = PagedCollection<VideoView>.Create(Link.ToCollection(nameof(GetVideo)),
                Videos.Items.ToArray(),
                Videos.TotalSize,
                pagingOptions,
                FormMetadata.FromModel(
                        new VideoForm(),
                        Link.ToForm(
                            nameof(VideoController.SaveVideo),
                            null,
                            Link.PostMethod,
                            Form.CreateRelation))
                );

            return collection;

        }

        [HttpPost(Name = nameof(SaveVideo))]
        public async Task<ActionResult> SaveVideo([FromForm] VideoForm VideoForm) 
        {
            var VideoId = await _iVideo.SaveVideo(VideoForm);

            return Created(Url.Link(nameof(CategoryController.GetCategory), new
            {
                VideoId
            }), VideoId);
        }
    }
}
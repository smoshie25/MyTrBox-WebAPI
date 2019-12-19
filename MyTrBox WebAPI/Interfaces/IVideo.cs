using MyTrBox_WebAPI.Model;
using MyTrBox_WebAPI.ModelViewHolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.Interfaces
{
    public interface IVideo
    {
        Task<VideoView> GetVideo(Guid id);
        Task<Guid> SaveVideo(VideoForm VideoForm);
        Task <PagedResult<VideoView>> GetVideoByArtist(Guid id,PagingOptions pagingOptions);
        Task <PagedResult<VideoView>> GetAllVideoAsync(PagingOptions pagingOptions);
    }
}

using MyTrBox_WebAPI.Model;
using MyTrBox_WebAPI.ModelViewHolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.Interfaces
{
    public interface IVideoAlbum
    {
        Task<Guid> SaveVideoAlbum(AlbumForm albumForm);

        Task<IEnumerable<VideoAlbumView>> GetAllVideoAlbumAsync();

        Task<PagedResult<VideoView>> GetVideoByAlbumAsync(Guid Id, PagingOptions pagingOptions);
    }
}

using MyTrBox_WebAPI.Model;
using MyTrBox_WebAPI.ModelViewHolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.Interfaces
{
    public interface ISongAlbum
    {
        Task<Guid> SaveSongAlbum(AlbumForm genre);

        Task<IEnumerable<SongAlbumView>> GetAllSongAlbumAsync();

        Task<PagedResult<SongView>> GetSongByAlbumAsync(Guid Id, PagingOptions pagingOptions);
    }
}

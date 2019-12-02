using MyTrBox_WebAPI.Model;
using MyTrBox_WebAPI.ModelViewHolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.Interfaces
{
    public interface IAlbum
    {
        /*Task<Guid> SaveAlbum(AlbumForm genre);

        Task<IEnumerable<AlbumView>> GetAllAlbumAsync();*/

        Task<PagedResult<ArtistView>> GetArtistByAlbumAsync(Guid Id, PagingOptions pagingOptions);
    }
}

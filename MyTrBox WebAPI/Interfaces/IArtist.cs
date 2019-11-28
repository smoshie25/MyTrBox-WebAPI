using MyTrBox_WebAPI.Model;
using MyTrBox_WebAPI.ModelViewHolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.Interfaces
{
    public interface IArtist
    {
        Task<PagedResult<SongView>> GetSongsByArtistAsync(Guid Id, PagingOptions pagingOptions);

        Task<Guid> SaveArtist(ArtistForm artist);
        Task <PagedResult<ArtistView>> GetAllArtistAsync(PagingOptions pagingOptions);
    }
}

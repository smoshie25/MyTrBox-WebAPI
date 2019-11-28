using MyTrBox_WebAPI.Model;
using MyTrBox_WebAPI.ModelViewHolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.Interfaces
{
    public interface ISong
    {
        Task<SongView> GetSong(Guid id);
        Task<Guid> SaveSong(SongForm songForm);
        Task <PagedResult<SongView>> GetAllSongAsync(PagingOptions pagingOptions);
    }
}

using MyTrBox_WebAPI.Model;
using MyTrBox_WebAPI.ModelViewHolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.Interfaces
{
    public interface IGenre
{
        Task<Guid> SaveGenre(GenreForm genre);

        Task<IEnumerable<GenreView>> GetAllGenreAsync();

        Task<PagedResult<SongView>> GetSongsByGenreAsync(Guid Id, PagingOptions pagingOptions);
    }
}

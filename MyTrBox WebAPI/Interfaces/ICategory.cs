using MyTrBox_WebAPI.Model;
using MyTrBox_WebAPI.ModelViewHolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.Interfaces
{
    public interface ICategory
{
        Task<Guid> SaveCategory(CategoryForm genre);

        Task<IEnumerable<CategoryView>> GetAllCategoryAsync();

        Task<PagedResult<ArtistView>> GetArtistByCategoryAsync(Guid Id, PagingOptions pagingOptions);
    }
}

using MyTrBox_WebAPI.Model;
using MyTrBox_WebAPI.ModelViewHolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.Interfaces
{
    public interface IPromo
{
        Task<Guid> SavePromo(PromoForm genre);

        Task<IEnumerable<PromoView>> GetAllPromoAsync();

        Task<PagedResult<PromoView>> GetPromoByPromoAsync(Guid Id, PagingOptions pagingOptions);
    }
}

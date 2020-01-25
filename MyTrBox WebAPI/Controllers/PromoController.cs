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
    public class PromoController : ControllerBase
    {
        private readonly IPromo _iPromo;
        private readonly PagingOptions _defaultPagingOptions;

        public PromoController(IPromo promo, IOptions<PagingOptions> defaultPagingOptions)
        {
            _iPromo = promo;
            _defaultPagingOptions = defaultPagingOptions.Value;
        }

        


        // GET: Genre/5
        [HttpGet("{Id}", Name = nameof(GetPromo))]
        public async Task<ActionResult<Collection<PromoView>>> GetPromo(Guid Id, [FromQuery] PagingOptions pagingOptions = null)
        {
            pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;
            pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;

            var artists = await _iPromo.GetPromoByPromoAsync(Id, pagingOptions);

            var collection = PagedCollection<PromoView>.Create(Link.ToCollection(nameof(GetPromo)),
                artists.Items.ToArray(),
                artists.TotalSize,
                pagingOptions,null
                );

            return collection;
        }

        [HttpPost(Name = nameof(SavPromo))]
        public async Task<ActionResult> SavPromo([FromForm] PromoForm promo) 
        {
            var promoId = await _iPromo.SavePromo(promo);

            return Created(Url.Link(nameof(PromoController.GetPromo), new
            {
                promoId
            }), promoId);
        }
    }
}
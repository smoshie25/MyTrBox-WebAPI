using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;
using MyTrBox_WebAPI.Interfaces;
using MyTrBox_WebAPI.Model;
using MyTrBox_WebAPI.ModelViewHolder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.Services
{
    public class PromoService : IPromo
    {
        private readonly AppDbContext db;
        private readonly IConfigurationProvider _mapingConfig;
        private readonly IHttpContextAccessor httpContextAccessor;
        public PromoService(AppDbContext appDbContext,
            IConfigurationProvider mapingConfig, IHttpContextAccessor _httpContextAccessor)
        {
            db = appDbContext;
            _mapingConfig = mapingConfig;
            httpContextAccessor = _httpContextAccessor;
        }

       

        public async Task<IEnumerable<PromoView>> GetAllPromoAsync()
        {
            var query = db.Promo.ProjectTo<PromoView>(_mapingConfig);

            return await query.ToArrayAsync();
        }

        public async Task<PagedResult<PromoView>> GetPromoByPromoAsync(Guid Id, PagingOptions pagingOptions)
        {
            var query = db.Promo.Where(x => x.Id == Id).ProjectTo<PromoView>(_mapingConfig).Include(x => x.Id);

            List<PromoView> promo = await query.ToListAsync();
            var allArtist = promo
                .Skip(pagingOptions.Offset.Value)
                .Take(pagingOptions.Limit.Value);
            return new PagedResult<PromoView>
            {
                Items = allArtist,
                TotalSize = promo.Count
            };
        }

        

        public async Task<Guid> SavePromo(PromoForm pomoForm)
        {
            var id = Guid.NewGuid();

            Guid typeId = Guid.Empty;

            switch (pomoForm.PromoType) {

                case PromoType.ALBUM:
                    var album = db.SongAlbum.SingleOrDefault(x => x.Id == pomoForm.TypeId);
                    if (album != null) {
                        typeId = album.Id;
                    }
                    break;
                case PromoType.SOMG:
                    var song = db.Song.SingleOrDefault(x => x.Id == pomoForm.TypeId);
                    if (song != null)
                    {
                        typeId = song.Id;
                    }
                    break;
                case PromoType.VIDEO:
                    var video = db.Video.SingleOrDefault(x => x.Id == pomoForm.TypeId);
                    if (video != null)
                    {
                        typeId = video.Id;
                    }
                    break;
                    
            }

            if (typeId == Guid.Empty) {

                var image = pomoForm.Image;
                // Saving Image on Server
                string extension = pomoForm.Image.FileName.Split(".").LastOrDefault();
                extension = image.FileName.Split(".").LastOrDefault();
                var url = httpContextAccessor.HttpContext?.Request?.GetDisplayUrl();
                url = httpContextAccessor.HttpContext?.Request?.GetDisplayUrl();

                var imageUrl = url.Split("song").FirstOrDefault() + "Uplodaed_Documents/promo/" + id + "." + extension;
                if (image.Length > 0)
                {
                    using (var fileStream = new FileStream("Uplodaed_Documents/promo/" + id + "." + extension, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }
                }
                else throw new InvalidOperationException("Invalid File");

                db.Promo.Add(new Promo
                {
                    Id = id,
                    Name = pomoForm.Name,
                    Image = imageUrl,
                    PromoType = pomoForm.PromoType,
                    TypeId = typeId
                });

                var created = await db.SaveChangesAsync();

                if(created < 1) throw new InvalidOperationException("Could not create Genre");
                return id;
            }



            return Guid.Empty;
        }

    }
}

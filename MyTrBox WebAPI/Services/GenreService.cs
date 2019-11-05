using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MyTrBox_WebAPI.Interfaces;
using MyTrBox_WebAPI.Model;
using MyTrBox_WebAPI.ModelViewHolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.Services
{
    public class GenreService : IGenre
    {
        private readonly AppDbContext db;
        private readonly IConfigurationProvider _mapingConfig;
        public GenreService(AppDbContext appDbContext,
            IConfigurationProvider mapingConfig)
        {
            db = appDbContext;
            _mapingConfig = mapingConfig;
        }

        public async Task<IEnumerable<GenreView>> GetAllGenreAsync()
        {
            var query = db.Genre.ProjectTo<GenreView>(_mapingConfig);

            return await query.ToArrayAsync();
        }

        public async Task<PagedResult<ArtistView>> GetArtistByGenreAsync(Guid Id, PagingOptions pagingOptions)
        {
            var query = db.Artist.Where(x => x.GenreId == Id).ProjectTo<ArtistView>(_mapingConfig).Include(x => x.Genre);

            List<ArtistView> artists = await query.ToListAsync();
            var allArtist = artists
                .Skip(pagingOptions.Offset.Value)
                .Take(pagingOptions.Limit.Value);
            return new PagedResult<ArtistView>
            {
                Items = allArtist,
                TotalSize = artists.Count
            };
        }

        public async Task<Guid> SaveGenre(GenreForm genre)
        {
            var id = new Guid();
            db.Genre.Add(new Genre {
                Id = id,
                Name = genre.Name,
                Description = genre.Description
            });

            var created = await db.SaveChangesAsync();

            if (created < 1) throw new InvalidOperationException("Could not create Genre");

            return id;
        }
    }
}

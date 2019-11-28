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
    public class ArtistService : IArtist
    {
        private readonly AppDbContext db;
        private readonly IConfigurationProvider _mapingConfig;
        private readonly IHttpContextAccessor httpContextAccessor;
        public ArtistService(AppDbContext appDbContext, 
            IConfigurationProvider mapingConfig, IHttpContextAccessor _httpContextAccessor) {
            db = appDbContext;
            _mapingConfig = mapingConfig;
            httpContextAccessor = _httpContextAccessor;
        }

        public async Task<PagedResult<ArtistView>> GetAllArtistAsync(PagingOptions pagingOptions)
        {
            var query = db.Artist.ProjectTo<ArtistView>(_mapingConfig).Include(x => x.Category);

            List<ArtistView> artists = await query.ToListAsync();
            var allArtist = artists
                .Skip(pagingOptions.Offset.Value)
                .Take(pagingOptions.Limit.Value);
            return new PagedResult<ArtistView> { 
                Items = allArtist,
                TotalSize = artists.Count
            };
        }

        public async Task<PagedResult<SongView>> GetSongsByArtistAsync(Guid Id, PagingOptions pagingOptions)
        {
            var query = db.Song.Where(x => x.ArtistId == Id).ProjectTo<SongView>(_mapingConfig).Include(x => x.Artist);

            List<SongView> songs = await query.ToListAsync();
            var allSongs = songs
                .Skip(pagingOptions.Offset.Value)
                .Take(pagingOptions.Limit.Value);
            return new PagedResult<SongView>
            {
                Items = allSongs,
                TotalSize = songs.Count
            };
        }

        public async Task<Guid> SaveArtist(ArtistForm artist)
        {
            var genre =  db.Category.SingleOrDefault(x => x.Id == artist.CategoryId);

            if (genre == null) throw new InvalidOperationException("Genere does not exist");

            var image = artist.Image;
            // Saving Image on Server
            var id = Guid.NewGuid();
            string extension = image.FileName.Split(".").LastOrDefault();
            var url = httpContextAccessor.HttpContext?.Request?.GetDisplayUrl();

            var imageUrl = url.Split("artist").FirstOrDefault() + "Uplodaed_Documents/artist/" + id + "." + extension;
            if (image.Length > 0)
            {
                using (var fileStream = new FileStream("Uplodaed_Documents/artist/" + id+"."+extension, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            } else throw new InvalidOperationException("Invalid File");



            db.Artist.Add(new Artist
            {
                id = id,
                Name = artist.Name,
                Image = imageUrl,
                CategoryId = genre.Id,
                Profile = artist.Profile,
                Category = genre
            });

            

            var created = await db.SaveChangesAsync();

            if (created < 1) throw new InvalidOperationException("Could not create Genre");

            return id;
        }
    }
}

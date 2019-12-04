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
    public class SongAlbumService : ISongAlbum
    {
        private readonly AppDbContext db;
        private readonly IConfigurationProvider _mapingConfig;
        public SongAlbumService(AppDbContext appDbContext,
            IConfigurationProvider mapingConfig)
        {
            db = appDbContext;
            _mapingConfig = mapingConfig;
        }

        public async Task<IEnumerable<SongAlbumView>> GetAllSongAlbumAsync()
        {
            var query = db.SongAlbum.ProjectTo<SongAlbumView>(_mapingConfig);

            return await query.ToArrayAsync();

        }

        public async Task<PagedResult<SongView>> GetSongByAlbumAsync(Guid Id, PagingOptions pagingOptions)
        {
           
            var query = db.Song.Where(x => x.AlbumId == Id).ProjectTo<SongView>(_mapingConfig);

            List<SongView> song = await query.ToListAsync();
            var allArtist = song
                .Skip(pagingOptions.Offset.Value)
                .Take(pagingOptions.Limit.Value);
            return new PagedResult<SongView>
            {
                Items = allArtist,
                TotalSize = song.Count
            };
        }


        public async Task<Guid> SaveSongAlbum(AlbumForm albumForm)
        {
            var artist = db.Artist.SingleOrDefault(x => x.id == albumForm.ArtistId);

            if (artist == null) throw new InvalidOperationException("Artist does not exist");

            var id = Guid.NewGuid();
            db.SongAlbum.Add(new SongAlbum
            {
                Id = id,
                Name = albumForm.Name,
                Description = albumForm.Description,
                ArtistId = artist.id,
                Artist = artist
            });

            var created = await db.SaveChangesAsync();

            if (created < 1) throw new InvalidOperationException("Could not create Album");

            return id;

        }
    }
}

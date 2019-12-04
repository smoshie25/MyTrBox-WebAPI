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
    public class VideoAlbumService : IVideoAlbum
    {
        private readonly AppDbContext db;
        private readonly IConfigurationProvider _mapingConfig;
        public VideoAlbumService(AppDbContext appDbContext,
            IConfigurationProvider mapingConfig)
        {
            db = appDbContext;
            _mapingConfig = mapingConfig;
        }

        public async Task<IEnumerable<VideoAlbumView>> GetAllVideoAlbumAsync()
        {
            var query = db.VideoAlbum.ProjectTo<VideoAlbumView>(_mapingConfig);

            return await query.ToArrayAsync();

        }
        

        public async Task<PagedResult<VideoView>> GetVideoByAlbumAsync(Guid Id, PagingOptions pagingOptions)
        {
            var query = db.Video.Where(x => x.VideoAlbumId == Id).ProjectTo<VideoView>(_mapingConfig);

            List<VideoView> artists = await query.ToListAsync();
            var allArtist = artists
                .Skip(pagingOptions.Offset.Value)
                .Take(pagingOptions.Limit.Value);
            return new PagedResult<VideoView>
            {
                Items = allArtist,
                TotalSize = artists.Count
            };
        }

        public async Task<Guid> SaveVideoAlbum(AlbumForm albumForm)
        {
            var artist = db.Artist.SingleOrDefault(x => x.id == albumForm.ArtistId);

            if (artist == null) throw new InvalidOperationException("Artist does not exist");

            var id = Guid.NewGuid();
            db.VideoAlbum.Add(new VideoAlbum
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

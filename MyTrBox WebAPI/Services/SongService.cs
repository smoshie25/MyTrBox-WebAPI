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
    public class SongService : ISong
    {
        private readonly AppDbContext db;
        private readonly IConfigurationProvider _mapingConfig;
        private readonly IHttpContextAccessor httpContextAccessor;
        public SongService(AppDbContext appDbContext, 
            IConfigurationProvider mapingConfig, IHttpContextAccessor _httpContextAccessor) {
            db = appDbContext;
            _mapingConfig = mapingConfig;
            httpContextAccessor = _httpContextAccessor;
        }

        public async Task<PagedResult<SongView>> GetAllSongAsync(PagingOptions pagingOptions)
        {
            var query = db.Song.ProjectTo<SongView>(_mapingConfig).Include(x => x.Artist);

            List<SongView> songs = await query.ToListAsync();
            var allArtist = songs
                .Skip(pagingOptions.Offset.Value)
                .Take(pagingOptions.Limit.Value);
            return new PagedResult<SongView> { 
                Items = allArtist,
                TotalSize = songs.Count
            };
        }

        public async Task<SongView> GetSong(Guid id)
        {
            var entity = await db.Artist.SingleOrDefaultAsync(x => x.id == id);

            if (entity == null) {
                return null;
            }
            var mapper = _mapingConfig.CreateMapper();
            return mapper.Map<SongView>(entity);

        }

        public async Task<Guid> SaveSong(SongForm songForm)
        {
            var artist =  db.Artist.SingleOrDefault(x => x.id == songForm.ArtistId);

            if (artist == null) throw new InvalidOperationException("Artist does not exist");
            
            var genre =  db.Genre.SingleOrDefault(x => x.Id == songForm.GenreId);

            if (genre == null) throw new InvalidOperationException("Genere does not exist");

            var album = db.SongAlbum.SingleOrDefault(x => x.Id == songForm.AlbumId);

            if (album == null) throw new InvalidOperationException("Album does not exist");

            var media = songForm.Media;
            // Saving Song on Server
            var id = Guid.NewGuid();
            string extension = media.FileName.Split(".").LastOrDefault();
            var url = httpContextAccessor.HttpContext?.Request?.GetDisplayUrl();

            var MediaUrl = url.Split("song").FirstOrDefault() + "Uplodaed_Documents/song/" + id + "." + extension;
            if (media.Length > 0)
            {
                using (var fileStream = new FileStream("Uplodaed_Documents/song/" + id+"."+extension, FileMode.Create))
                {
                    media.CopyTo(fileStream);
                }
            } else throw new InvalidOperationException("Invalid File");


            var image = songForm.Image;
            // Saving Image on Server
            extension = image.FileName.Split(".").LastOrDefault();
            url = httpContextAccessor.HttpContext?.Request?.GetDisplayUrl();

            var imageUrl = url.Split("song").FirstOrDefault() + "Uplodaed_Documents/song/" + id + "." + extension;
            if (image.Length > 0)
            {
                using (var fileStream = new FileStream("Uplodaed_Documents/song/" + id + "." + extension, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }
            else throw new InvalidOperationException("Invalid File");




            db.Song.Add(new Song
            {
                Id = id,
                Title = songForm.Title,
                Media = MediaUrl,
                ArtistId = artist.id,
                GenreId = genre.Id,
                Genre = genre,
                Artist = artist,
                Image = imageUrl,
                AlbumId = album.Id,
                Album = album
            });

            

            var created = await db.SaveChangesAsync();

            if (created < 1) throw new InvalidOperationException("Could not create Genre");

            return id;
        }
    }
}

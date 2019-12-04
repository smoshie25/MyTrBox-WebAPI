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
    public class VideoService : IVideo
    {
        private readonly AppDbContext db;
        private readonly IConfigurationProvider _mapingConfig;
        private readonly IHttpContextAccessor httpContextAccessor;
        public VideoService(AppDbContext appDbContext, 
            IConfigurationProvider mapingConfig, IHttpContextAccessor _httpContextAccessor) {
            db = appDbContext;
            _mapingConfig = mapingConfig;
            httpContextAccessor = _httpContextAccessor;
        }

        public async Task<PagedResult<VideoView>> GetAllVideoAsync(PagingOptions pagingOptions)
        {
            var query = db.Video.ProjectTo<VideoView>(_mapingConfig).Include(x => x.Artist);

            List<VideoView> Videos = await query.ToListAsync();
            var allArtist = Videos
                .Skip(pagingOptions.Offset.Value)
                .Take(pagingOptions.Limit.Value);
            return new PagedResult<VideoView> { 
                Items = allArtist,
                TotalSize = Videos.Count
            };
        }

        public async Task<VideoView> GetVideo(Guid id)
        {
            var entity = await db.Artist.SingleOrDefaultAsync(x => x.id == id);

            if (entity == null) {
                return null;
            }
            var mapper = _mapingConfig.CreateMapper();
            return mapper.Map<VideoView>(entity);

        }

        public async Task<Guid> SaveVideo(VideoForm form)
        {
            var artist =  db.Artist.SingleOrDefault(x => x.id == form.ArtistId);

            if (artist == null) throw new InvalidOperationException("Artist does not exist");
            
            var genre =  db.Genre.SingleOrDefault(x => x.Id == form.GenreId);

            if (genre == null) throw new InvalidOperationException("Genere does not exist");
            
            var album =  db.VideoAlbum.SingleOrDefault(x => x.Id == form.AlbumId);

            if (album == null) throw new InvalidOperationException("Album does not exist");

            var media = form.Media;
            // Saving Video on Server
            var id = Guid.NewGuid();
            string extension = media.FileName.Split(".").LastOrDefault();
            var url = httpContextAccessor.HttpContext?.Request?.GetDisplayUrl();

            var MediaUrl = url.Split("artist").FirstOrDefault() + "Uplodaed_Documents/Video/" + id + "." + extension;
            if (media.Length > 0)
            {
                using (var fileStream = new FileStream("Uplodaed_Documents/Video/" + id+"."+extension, FileMode.Create))
                {
                    media.CopyTo(fileStream);
                }
            } else throw new InvalidOperationException("Invalid File");


            var image = form.Image;
            // Saving Image on Server
            extension = image.FileName.Split(".").LastOrDefault();
            url = httpContextAccessor.HttpContext?.Request?.GetDisplayUrl();

            var imageUrl = url.Split("artist").FirstOrDefault() + "Uplodaed_Documents/Video/" + id + "." + extension;
            if (image.Length > 0)
            {
                using (var fileStream = new FileStream("Uplodaed_Documents/Video/" + id + "." + extension, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }
            else throw new InvalidOperationException("Invalid File");




            db.Video.Add(new Video
            {
                Id = id,
                Title = form.Title,
                Media = MediaUrl,
                ArtistId = artist.id,
                GenreId = genre.Id,
                Genre = genre,
                Artist = artist,
                Image = imageUrl,
                VideoAlbumId = album.Id,
                VideoAlbum = album
            });

            

            var created = await db.SaveChangesAsync();

            if (created < 1) throw new InvalidOperationException("Could not create Genre");

            return id;
        }
    }
}

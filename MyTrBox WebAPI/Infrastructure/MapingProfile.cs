using AutoMapper;
using MyTrBox_WebAPI.Model;
using MyTrBox_WebAPI.ModelViewHolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.Infrastructure
{
    public class MapingProfile : Profile
    {
        public MapingProfile() {
            CreateMap<Artist, ArtistView>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                    Link.To(
                        nameof(Controllers.ArtistController.GetArtist),
                        new { artistID = src.id })));

            CreateMap<Category, CategoryView>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                    Link.To(
                        nameof(Controllers.CategoryController.GetGenreArtist),
                        new { genreID = src.Id })));

            CreateMap<Genre, GenreView>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                    Link.To(
                        nameof(Controllers.GenreController.GetGenre),
                        new { genreID = src.Id })));

            CreateMap<Song, SongView>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                    Link.To(
                        nameof(Controllers.SongController.GetSongById),
                        new { genreID = src.Id })));

            CreateMap<Video, VideoView>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                    Link.To(
                        nameof(Controllers.VideoController.GetVideoById),
                        new { genreID = src.Id })));

            CreateMap<SongAlbum, SongAlbumView>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                    Link.To(
                        nameof(Controllers.SongAlbumController.GetSongAlbum),
                        new { genreID = src.Id })));

            CreateMap<VideoAlbum, VideoAlbumView>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                    Link.To(
                        nameof(Controllers.VideoAlbumController.GetVideoAlbum),
                        new { genreID = src.Id })));
        }
    }
}

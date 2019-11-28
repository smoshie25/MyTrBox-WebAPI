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
                        nameof(Controllers.ArtistController.GetArtistSongs),
                        new { artistID = src.id }))); 

            CreateMap<Category, CategoryView>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                    Link.To(
                        nameof(Controllers.CategoryController.GetGenreArtist),
                        new { genreID = src.Id }))); 

            CreateMap<Song, SongView>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                    Link.To(
                        nameof(Controllers.SongController.GetSongById),
                        new { genreID = src.SongId }))); 
        }
    }
}

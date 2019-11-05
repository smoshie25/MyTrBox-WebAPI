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
                        nameof(Controllers.ArtistController.GetArtistById),
                        new { artistID = src.id }))); 

            CreateMap<Genre, GenreView>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                    Link.To(
                        nameof(Controllers.GenreController.GetGenreArtist),
                        new { genreID = src.Id }))); 
        }
    }
}

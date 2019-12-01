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
                        new { artistID = src.id })))
                .ForMember(dest => dest.Form, opt => opt.MapFrom(src =>
                    FormMetadata.FromModel(
                        new CategoryForm(),
                        Link.ToForm(
                            nameof(Controllers.ArtistController.SaveArtist),
                            null,
                            Link.PostMethod,
                            Form.CreateRelation))));

            CreateMap<Category, CategoryView>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                    Link.To(
                        nameof(Controllers.CategoryController.GetGenreArtist),
                        new { genreID = src.Id })));
                /*.ForMember(dest => dest.Form, opt => opt.MapFrom(src =>
                    FormMetadata.FromModel(
                        new CategoryForm(),
                        Link.ToForm(
                            nameof(Controllers.CategoryController.PostCategory),
                            null,
                            Link.PostMethod,
                            Form.CreateRelation))));*/

            CreateMap<Genre, GenreView>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                    Link.To(
                        nameof(Controllers.GenreController.GetGenre),
                        new { genreID = src.Id }))); 

            CreateMap<Song, SongView>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                    Link.To(
                        nameof(Controllers.SongController.GetSongById),
                        new { genreID = src.Id })))
                .ForMember(dest => dest.Song, opt => opt.MapFrom(src =>
                    FormMetadata.FromModel(
                        new SongForm(),
                        Link.ToForm(
                            nameof(Controllers.SongController.SaveSong),
                            null,
                            Link.PostMethod,
                            Form.CreateRelation))));
        }
    }
}

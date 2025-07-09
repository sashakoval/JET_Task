using AutoMapper;
using JET_Task.Application.DTOs;
using JET_Task.Domain.Entities;

namespace JET_Task.Application.Mapping
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            // Map Movie entity to MovieDto
            CreateMap<Movie, MovieDto>()
                .ForMember(dest => dest.ImdbId, opt => opt.MapFrom(src => src.ImdbId.Value));

            // Map SearchQuery entity to SearchQueryDto
            CreateMap<SearchQuery, SearchQueryDto>()
                .ForMember(dest => dest.Query, opt => opt.MapFrom(src => src.Query.Value));
        }
    }
} 
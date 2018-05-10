using AutoMapper;
using Model.DB;
using Model.DTO;
using System.Collections.Generic;

namespace BAL.IoC
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
              : this("MyProfile")
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, options => options.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, options => options.MapFrom(src => src.Email));
                cfg.CreateMap<List<User>, List<UserDTO>>();

                cfg.CreateMap<AboutUnion, AboutUnionDTO>();
                cfg.CreateMap<List<AboutUnion>, List<AboutUnionDTO>>();

                cfg.CreateMap<AboutUs, AboutUsDTO>();
                cfg.CreateMap<List<AboutUs>, List<AboutUsDTO>>();

                cfg.CreateMap<Carousel, CarouselDTO>();
                cfg.CreateMap<List<Carousel>, List<CarouselDTO>>();

                cfg.CreateMap<Contacts, ContactsDTO>();
                cfg.CreateMap<List<Contacts>, List<ContactsDTO>>();

                cfg.CreateMap<Media, MediaDTO>();
                cfg.CreateMap<List<Media>, List<MediaDTO>>();

                cfg.CreateMap<News, NewsDTO>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.Day, options => options.MapFrom(src => src.Day))
                .ForMember(dest => dest.Month, options => options.MapFrom(src => src.Month))
                .ForMember(dest => dest.Image_Id, options => options.MapFrom(src => src.Image_Id))
                .ForMember(dest => dest.Text, options => options.MapFrom(src => src.Text))
                .ForMember(dest => dest.Title, options => options.MapFrom(src => src.Title))
                .ForMember(dest => dest.Year, options => options.MapFrom(src => src.Year));
                cfg.CreateMap<List<News>, List<NewsDTO>>();

                cfg.CreateMap<Partners, PartnersDTO>();
                cfg.CreateMap<List<Partners>, List<PartnersDTO>>();

                cfg.CreateMap<Projects, ProjectsDTO>();
                cfg.CreateMap<List<Projects>, List<ProjectsDTO>>();

                cfg.CreateMap<Video, VideoDTO>();
                cfg.CreateMap<List<Video>, List<VideoDTO>>();

                cfg.CreateMap<FaceBook, FaceBookDTO>();
                cfg.CreateMap<List<FaceBook>, List<FaceBookDTO>>();

                cfg.CreateMap<Image, ImageDTO>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.ImagePath, options => options.MapFrom(src => src.ImagePath));
                cfg.CreateMap<List<Image>, List<ImageDTO>>();
            });
        }

        protected AutoMapperProfileConfiguration(string profileName)
            : base(profileName)
        {

        }
    }
}

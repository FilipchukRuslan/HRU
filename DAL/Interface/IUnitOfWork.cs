using Model.DB;
using System;
using Microsoft.AspNetCore.Identity;

namespace DAL.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<User> UserRepo { get; }
        IBaseRepository<IdentityRole> RoleRepo { get; }

        IBaseRepository<Media> MediaRepo { get; }
        IBaseRepository<Projects> ProjectsRepo { get; }
        IBaseRepository<Partners> PartnersRepo { get; }
        IBaseRepository<News> NewsRepo { get; }
        IBaseRepository<Carousel> CarouselRepo { get; }
        IBaseRepository<Image> ImageRepo { get; }
        IBaseRepository<Contacts> ContactsRepo { get; }
        IBaseRepository<AboutUs> AboutUsRepo { get; }
        IBaseRepository<AboutUnion> AboutUnionRepo { get; }
        IBaseRepository<Video> VideoRepo { get; }
        IBaseRepository<FaceBook> FaceBookRepo { get; }
        int Save();
    }
}

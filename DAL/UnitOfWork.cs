using System;
using DAL.Repositories;
using Model.DB;
using DAL.Interface;
using Microsoft.AspNetCore.Identity;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HRUDbContext context;

        private IBaseRepository<IdentityRole> roleRepo;
        private IBaseRepository<User> userRepo;
        private IBaseRepository<Image> imageRepo;
        private IBaseRepository<News> newsRepo;
        private IBaseRepository<Projects> projectsRepo;
        private IBaseRepository<Carousel> carouselRepo;
        private IBaseRepository<Partners> partnersRepo;
        private IBaseRepository<Media> mediaRepo;
        private IBaseRepository<AboutUnion> aboutUnionRepo;
        private IBaseRepository<Contacts> contactsRepo;
        private IBaseRepository<FaceBook> faceBookRepo;
        private IBaseRepository<Video> videoRepo;
        private IBaseRepository<Messages> messagesRepo;
        private IBaseRepository<AbstractInfo> abstractInfoRepo;

        public UnitOfWork(HRUDbContext context)
        {
            this.context = context;
        }

        public IBaseRepository<IdentityRole> RoleRepo
        {
            get
            {
                if (roleRepo == null) { roleRepo = new BaseRepository<IdentityRole>(context); }
                return roleRepo;
            }
        }

        public IBaseRepository<User> UserRepo
        {
            get
            {
                if (userRepo == null) { userRepo = new BaseRepository<User>(context); }
                return userRepo;
            }
        }

        public IBaseRepository<Media> MediaRepo
        {
            get
            {
                if (mediaRepo == null) { mediaRepo = new BaseRepository<Media>(context); }
                return mediaRepo;
            }
        }

        public IBaseRepository<Projects> ProjectsRepo
        {
            get
            {
                if (projectsRepo == null) { projectsRepo = new BaseRepository<Projects>(context); }
                return projectsRepo;
            }
        }

        public IBaseRepository<Partners> PartnersRepo
        {
            get
            {
                if (partnersRepo == null) { partnersRepo = new BaseRepository<Partners>(context); }
                return partnersRepo;
            }
        }

        public IBaseRepository<News> NewsRepo
        {
            get
            {
                if (newsRepo == null) { newsRepo = new BaseRepository<News>(context); }
                return newsRepo;
            }
        }

       
        public IBaseRepository<Carousel> CarouselRepo
        {
            get
            {
                if (carouselRepo == null) { carouselRepo = new BaseRepository<Carousel>(context); }
                return carouselRepo;
            }
        }

        public IBaseRepository<Image> ImageRepo
        {
            get
            {
                if (imageRepo == null) { imageRepo = new BaseRepository<Image>(context); }
                return imageRepo;
            }
        }
        public IBaseRepository<Contacts> ContactsRepo
        {
            get
            {
                if (contactsRepo == null) { contactsRepo = new BaseRepository<Contacts>(context); }
                return contactsRepo;
            }
        }
        
        public IBaseRepository<AboutUnion> AboutUnionRepo
        {
            get
            {
                if (aboutUnionRepo == null) { aboutUnionRepo = new BaseRepository<AboutUnion>(context); }
                return aboutUnionRepo;
            }
        }

        public IBaseRepository<FaceBook> FaceBookRepo
        {
            get
            {
                if (faceBookRepo == null) { faceBookRepo = new BaseRepository<FaceBook>(context); }
                return faceBookRepo;
            }
        }

        public IBaseRepository<Video> VideoRepo
        {
            get
            {
                if (videoRepo == null) { videoRepo = new BaseRepository<Video>(context); }
                return videoRepo;
            }
        }


        public IBaseRepository<Messages> MessagesRepo
        {
            get
            {
                if (messagesRepo == null) { messagesRepo = new BaseRepository<Messages>(context); }
                return messagesRepo;
            }
        }

        public IBaseRepository<AbstractInfo> AbstractInfoRepo
        {
            get
            {
                if (abstractInfoRepo == null) { abstractInfoRepo = new BaseRepository<AbstractInfo>(context); }
                return abstractInfoRepo;
            }
        }
        public int Save()
        {
            return context.SaveChanges();
        }

        private bool isDisposed = false;

        protected virtual void Grind(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            isDisposed = true;
        }

        public void Dispose()
        {
            Grind(true);
            GC.SuppressFinalize(this);
        }
    }
}

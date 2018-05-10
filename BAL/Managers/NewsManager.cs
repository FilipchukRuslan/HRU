using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BAL.Interfaces;
using DAL.Interface;
using Microsoft.AspNetCore.Identity;
using Model.DB;
using Model.Entity;

namespace BAL.Managers
{


    public class NewsManager : INewsManager
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;
        private IImageManager imageManager;
        private UserManager<User> userManager;
        
        public NewsManager(IUnitOfWork unitOfWork, IMapper mapper, IImageManager imageManager, UserManager<User> userManager)
        {
            this.userManager = userManager;
            this.imageManager = imageManager;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<NewsDTO> GetAll()
        {
            return mapper.Map<List<NewsDTO>>(unitOfWork.NewsRepo.GetAll());
        }

        public NewsDTO GetById(int id)
        {
            return mapper.Map<NewsDTO>(unitOfWork.NewsRepo.GetById(id));
        }

        public virtual IEnumerable<NewsDTO> Get(
            Expression<Func<News, bool>> filter = null,
            Func<IQueryable<News>,
            IOrderedQueryable<News>> orderBy = null,
            string includeProperties = "")
        {
            return mapper.Map<List<NewsDTO>>(unitOfWork.NewsRepo.Get(filter, orderBy, includeProperties));
        }

        public void Insert(NewsDTO entity)
        {
            unitOfWork.NewsRepo.Insert(mapper.Map<News>(entity));
            unitOfWork.Save();
        }

        public void Update(NewsDTO entityToUpdate)
        {
            unitOfWork.NewsRepo.Update(mapper.Map<News>(entityToUpdate));
            unitOfWork.Save();
        }


        public void Delete(NewsDTO entityToDelete)
        {
            unitOfWork.NewsRepo.Delete(mapper.Map<News>(entityToDelete));
            unitOfWork.Save();
        }



    }
}
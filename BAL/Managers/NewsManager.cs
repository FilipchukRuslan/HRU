using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BAL.Interfaces;
using Common;
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

        public NewsManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<News> GetAll()
        {
            return unitOfWork.NewsRepo.GetAll();
        }

        public News GetById(int id)
        {
            return unitOfWork.NewsRepo.GetById(id);
        }

        public virtual IEnumerable<News> Get(Expression<Func<News, bool>> filter = null,
                                     Func<IQueryable<News>,
                                     IOrderedQueryable<News>> orderBy = null,
                                     string includeProperties = "")
        {
            return unitOfWork.NewsRepo.Get(filter, orderBy, includeProperties);
        }

        public void Insert(News entity)
        {
            unitOfWork.NewsRepo.Insert(entity);
            unitOfWork.Save();
        }

        public void Update(News entity)
        {
            var article = unitOfWork.NewsRepo.GetById(entity.Id);
            article.Title = entity.Title;
            article.Text = entity.Text;
            article.Day = DateTime.Today.Day;
            article.Month = Enum.GetName(typeof(MonthEnum), DateTime.Today.Month - 1);
            unitOfWork.NewsRepo.Update(article);
            unitOfWork.Save();
        }

        public void Delete(News entityToDelete)
        {
            unitOfWork.NewsRepo.Delete(entityToDelete);
            unitOfWork.Save();
        }



    }
}
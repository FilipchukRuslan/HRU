using System;
using DAL.Interface;
using BAL.Interfaces;
using System.Collections.Generic;
using Model.DTO;
using System.Linq.Expressions;
using System.Linq;
using Model.DB;
using AutoMapper;


namespace BAL.Managers
{
    public class CarouselManager :  ICarouselManager 
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public CarouselManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Carousel> GetAll()
        {
            return unitOfWork.CarouselRepo.GetAll();
        }

        public Carousel GetById(int id)
        {
            return unitOfWork.CarouselRepo.GetById(id);
        }

        public virtual IEnumerable<Carousel> Get(
            Expression<Func<Carousel, bool>> filter = null,
            Func<IQueryable<Carousel>,
            IOrderedQueryable<Carousel>> orderBy = null,
            string includeProperties = "")
        {
            return unitOfWork.CarouselRepo.Get(filter, orderBy, includeProperties);
        }

        public void Insert(Carousel entity)
        {
            unitOfWork.CarouselRepo.Insert(entity);
            unitOfWork.Save();
        }

        public void Update(Carousel entityToUpdate)
        {
            unitOfWork.CarouselRepo.Update(entityToUpdate);
            unitOfWork.Save();
        }
        

        public void Delete(Carousel entityToDelete)
        {
            unitOfWork.CarouselRepo.Delete(entityToDelete);
            unitOfWork.Save();
        }
    }
}

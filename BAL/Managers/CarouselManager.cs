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

        public IEnumerable<CarouselDTO> GetAll()
        {
            return mapper.Map<List<CarouselDTO>>(unitOfWork.CarouselRepo.GetAll());
        }

        public CarouselDTO GetById(int id)
        {
            return mapper.Map<CarouselDTO>(unitOfWork.CarouselRepo.GetById(id));
        }

        public virtual IEnumerable<CarouselDTO> Get(
            Expression<Func<Carousel, bool>> filter = null,
            Func<IQueryable<Carousel>,
            IOrderedQueryable<Carousel>> orderBy = null,
            string includeProperties = "")
        {
            return mapper.Map<List<CarouselDTO>>(unitOfWork.CarouselRepo.Get(filter, orderBy, includeProperties));
        }

        public void Insert(CarouselDTO entity)
        {
            unitOfWork.CarouselRepo.Insert(mapper.Map<Carousel>(entity));
            unitOfWork.Save();
        }

        public void Update(CarouselDTO entityToUpdate)
        {
            unitOfWork.CarouselRepo.Update(mapper.Map<Carousel>(entityToUpdate));
            unitOfWork.Save();
        }
        

        public void Delete(CarouselDTO entityToDelete)
        {
            unitOfWork.CarouselRepo.Delete(mapper.Map<Carousel>(entityToDelete));
            unitOfWork.Save();
        }
    }
}

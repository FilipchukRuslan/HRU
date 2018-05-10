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
    public class ImageManager : IImageManager
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public ImageManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<Image> GetAll()
        {
            return unitOfWork.ImageRepo.GetAll();
        }

        public Image GetById(int id)
        {
            return mapper.Map<Image>(unitOfWork.ImageRepo.GetById(id));
        }

        public virtual IEnumerable<Image> Get(
            Expression<Func<Image, bool>> filter = null,
            Func<IQueryable<Image>,
            IOrderedQueryable<Image>> orderBy = null,
            string includeProperties = "")
        {
            return unitOfWork.ImageRepo.Get(filter, orderBy, includeProperties);
        }

        public void Insert(Image entity)
        {
            unitOfWork.ImageRepo.Insert(entity);
            unitOfWork.Save();
        }

        public void Update(Image entityToUpdate)
        {
            unitOfWork.ImageRepo.Update(entityToUpdate);
            unitOfWork.Save();
        }


        public void Delete(Image entityToDelete)
        {
            unitOfWork.ImageRepo.Delete(entityToDelete);
            unitOfWork.Save();
        }


    }
}

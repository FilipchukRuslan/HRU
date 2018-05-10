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
        public IEnumerable<ImageDTO> GetAll()
        {
            return mapper.Map<List<ImageDTO>>(unitOfWork.ImageRepo.GetAll());
        }

        public ImageDTO GetById(int id)
        {
            return mapper.Map<ImageDTO>(unitOfWork.ImageRepo.GetById(id));
        }

        public virtual IEnumerable<ImageDTO> Get(
            Expression<Func<Image, bool>> filter = null,
            Func<IQueryable<Image>,
            IOrderedQueryable<Image>> orderBy = null,
            string includeProperties = "")
        {
            return mapper.Map<List<ImageDTO>>(unitOfWork.ImageRepo.Get(filter, orderBy, includeProperties));
        }

        public void Insert(ImageDTO entity)
        {
            unitOfWork.ImageRepo.Insert(mapper.Map<Image>(entity));
            unitOfWork.Save();
        }

        public void Update(ImageDTO entityToUpdate)
        {
            unitOfWork.ImageRepo.Update(mapper.Map<Image>(entityToUpdate));
            unitOfWork.Save();
        }


        public void Delete(ImageDTO entityToDelete)
        {
            unitOfWork.ImageRepo.Delete(mapper.Map<Image>(entityToDelete));
            unitOfWork.Save();
        }


    }
}

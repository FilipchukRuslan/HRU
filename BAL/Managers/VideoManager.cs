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
    public class VideoManager : IVideoManager
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public VideoManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Video> GetAll()
        {
            return unitOfWork.VideoRepo.GetAll();
        }
       
        public Video GetById(int id)
        {
            return unitOfWork.VideoRepo.GetById(id);
        }

        public virtual IEnumerable<Video> Get(
            Expression<Func<Video, bool>> filter = null,
            Func<IQueryable<Video>,
            IOrderedQueryable<Video>> orderBy = null,
            string includeProperties = "")
        {
            return mapper.Map<List<Video>>(unitOfWork.VideoRepo.Get(filter, orderBy, includeProperties));
        }

        public void Insert(Video entity)
        {
            unitOfWork.VideoRepo.Insert(entity);
            unitOfWork.Save();
        }

        public void Update(Video entityToUpdate)
        {
            unitOfWork.VideoRepo.Update(entityToUpdate);
            unitOfWork.Save();
        }
        
        public void Delete(Video entityToDelete)
        {
            unitOfWork.VideoRepo.Delete(entityToDelete);
            unitOfWork.Save();
        }
    }
}

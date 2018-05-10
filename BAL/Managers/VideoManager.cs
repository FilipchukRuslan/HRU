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

        public IEnumerable<VideoDTO> GetAll()
        {
            return mapper.Map<List<VideoDTO>>(unitOfWork.VideoRepo.GetAll());
        }
       
        public VideoDTO GetById(int id)
        {
            return mapper.Map<VideoDTO>(unitOfWork.VideoRepo.GetById(id));
        }

        public virtual IEnumerable<VideoDTO> Get(
            Expression<Func<Video, bool>> filter = null,
            Func<IQueryable<Video>,
            IOrderedQueryable<Video>> orderBy = null,
            string includeProperties = "")
        {
            return mapper.Map<List<VideoDTO>>(unitOfWork.VideoRepo.Get(filter, orderBy, includeProperties));
        }

        public void Insert(VideoDTO entity)
        {
            unitOfWork.VideoRepo.Insert(mapper.Map<Video>(entity));
            unitOfWork.Save();
        }

        public void Update(VideoDTO entityToUpdate)
        {
            unitOfWork.VideoRepo.Update(mapper.Map<Video>(entityToUpdate));
            unitOfWork.Save();
        }
        
        public void Delete(VideoDTO entityToDelete)
        {
            unitOfWork.VideoRepo.Delete(mapper.Map<Video>(entityToDelete));
            unitOfWork.Save();
        }
    }
}

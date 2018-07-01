using AutoMapper;
using BAL.Interfaces;
using DAL.Interface;
using Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BAL.Managers
{
    public class MediaManager: IMediaManager
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public MediaManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Media> GetAll()
        {
            return unitOfWork.MediaRepo.GetAll();
        }

        public Media GetById(int id)
        {
            return unitOfWork.MediaRepo.GetById(id);
        }

        public virtual IEnumerable<Media> Get(Expression<Func<Media, bool>> filter = null,
                                     Func<IQueryable<Media>,
                                     IOrderedQueryable<Media>> orderBy = null,
                                     string includeProperties = "")
        {
            return unitOfWork.MediaRepo.Get(filter, orderBy, includeProperties);
        }

        public void Insert(Media entity)
        {
            unitOfWork.MediaRepo.Insert(entity);
            unitOfWork.Save();
        }
        
        public void Update(Media entity)
        {
            var media = unitOfWork.MediaRepo.GetById(entity.Id);
            media.MediaName = entity.MediaName;
            media.Text = entity.Text;
            unitOfWork.Save();
        }
        public void Delete(Media entityToDelete)
        {
            unitOfWork.MediaRepo.Delete(entityToDelete);
            unitOfWork.Save();
        }

    }
}

using DAL.Interface;
using BAL.Interfaces;
using System.Collections.Generic;
using Model.DTO;
using Model.DB;
using AutoMapper;
using System.Linq.Expressions;
using System.Linq;
using System;

namespace BAL.Managers
{
    public class FaceBookManager : IFaceBookManager
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public FaceBookManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<FaceBook> GetAll()
        {
            return unitOfWork.FaceBookRepo.GetAll();
        }

        public FaceBook GetById(int id)
        {
            return unitOfWork.FaceBookRepo.GetById(id);
        }

        public virtual IEnumerable<FaceBook> Get(
            Expression<Func<FaceBook, bool>> filter = null,
            Func<IQueryable<FaceBook>,
            IOrderedQueryable<FaceBook>> orderBy = null,
            string includeProperties = "")
        {
            return unitOfWork.FaceBookRepo.Get(filter, orderBy, includeProperties);
        }

        public void Insert(string entity)
        {
            unitOfWork.FaceBookRepo.Insert(new FaceBook() { FBPost = entity});
            unitOfWork.Save();
        }

        public void Update(FaceBook entityToUpdate)
        {
            unitOfWork.FaceBookRepo.Update(entityToUpdate);
            unitOfWork.Save();
        }
        
        public void Delete(FaceBook entityToDelete)
        {
            unitOfWork.FaceBookRepo.Delete(entityToDelete);
            unitOfWork.Save();
        }
    }
}

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

        public IEnumerable<FaceBookDTO> GetAll()
        {
            return mapper.Map<List<FaceBookDTO>>(unitOfWork.FaceBookRepo.GetAll());
        }

        public FaceBookDTO GetById(int id)
        {
            return mapper.Map<FaceBookDTO>(unitOfWork.FaceBookRepo.GetById(id));
        }

        public virtual IEnumerable<FaceBookDTO> Get(
            Expression<Func<FaceBook, bool>> filter = null,
            Func<IQueryable<FaceBook>,
            IOrderedQueryable<FaceBook>> orderBy = null,
            string includeProperties = "")
        {
            return mapper.Map<List<FaceBookDTO>>(unitOfWork.FaceBookRepo.Get(filter, orderBy, includeProperties));
        }

        public void Insert(FaceBookDTO entity)
        {
            unitOfWork.FaceBookRepo.Insert(mapper.Map<FaceBook>(entity));
            unitOfWork.Save();
        }

        public void Update(FaceBookDTO entityToUpdate)
        {
            unitOfWork.FaceBookRepo.Update(mapper.Map<FaceBook>(entityToUpdate));
            unitOfWork.Save();
        }
        
        public void Delete(FaceBookDTO entityToDelete)
        {
            unitOfWork.FaceBookRepo.Delete(mapper.Map<FaceBook>(entityToDelete));
            unitOfWork.Save();
        }
    }
}

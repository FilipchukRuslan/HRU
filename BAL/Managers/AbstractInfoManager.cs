using AutoMapper;
using BAL.Interfaces;
using DAL.Interface;
using Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BAL.Managers
{
    public class AbstractInfoManager : IAbstractInfoManager
    {
        private IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AbstractInfoManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<AbstractInfo> GetAll()
        {
            return unitOfWork.AbstractInfoRepo.GetAll();
        }

        public AbstractInfo GetById(int id)
        {
            return unitOfWork.AbstractInfoRepo.GetById(id);
        }

        public virtual IEnumerable<AbstractInfo> Get(Expression<Func<AbstractInfo, bool>> filter = null,
                                     Func<IQueryable<AbstractInfo>,
                                     IOrderedQueryable<AbstractInfo>> orderBy = null,
                                     string includeProperties = "")
        {
            return unitOfWork.AbstractInfoRepo.Get(filter, orderBy, includeProperties);
        }

        public void Insert(AbstractInfo entity)
        {
            unitOfWork.AbstractInfoRepo.Insert(entity);
            unitOfWork.Save();
        }

        public void Update(AbstractInfo entity)
        {
            var abstractInfo = unitOfWork.AbstractInfoRepo.GetById(entity.Id);
            abstractInfo.Text = entity.Text;
            unitOfWork.Save();
        }
        public void Delete(AbstractInfo entityToDelete)
        {
            unitOfWork.AbstractInfoRepo.Delete(entityToDelete);
            unitOfWork.Save();
        }
    }
}

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
    public class PartnersManager : IPartnersManager
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public PartnersManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Partners> GetAll()
        {
            return unitOfWork.PartnersRepo.GetAll();
        }

        public Partners GetById(int id)
        {
            return unitOfWork.PartnersRepo.GetById(id);
        }

        public virtual IEnumerable<Partners> Get(Expression<Func<Partners, bool>> filter = null,
                                     Func<IQueryable<Partners>,
                                     IOrderedQueryable<Partners>> orderBy = null,
                                     string includeProperties = "")
        {
            return unitOfWork.PartnersRepo.Get(filter, orderBy, includeProperties);
        }

        public void Insert(Partners entity)
        {
            unitOfWork.PartnersRepo.Insert(entity);
            unitOfWork.Save();
        }

        public void Update(Partners entity)
        {
            var partner = unitOfWork.PartnersRepo.GetById(entity.Id);
            partner.ParnerName = entity.ParnerName;
            partner.ParnerAbout = entity.ParnerAbout;
            partner.Image_Id = entity.Image_Id;
            unitOfWork.Save();
        }

        public void Delete(Partners entityToDelete)
        {
            unitOfWork.PartnersRepo.Delete(entityToDelete);
            unitOfWork.Save();
        }



    }
}

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
    public class AboutUnionManager : IAboutUnionManager
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public AboutUnionManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<AboutUnion> GetAll()
        {
            return unitOfWork.AboutUnionRepo.GetAll();
        }

        public AboutUnion GetById(int id)
        {
            return unitOfWork.AboutUnionRepo.GetById(id);
        }

        public virtual IEnumerable<AboutUnion> Get(Expression<Func<AboutUnion, bool>> filter = null,
                                     Func<IQueryable<AboutUnion>,
                                     IOrderedQueryable<AboutUnion>> orderBy = null,
                                     string includeProperties = "")
        {
            return unitOfWork.AboutUnionRepo.Get(filter, orderBy, includeProperties);
        }

        public void Insert(AboutUnion entity)
        {
            unitOfWork.AboutUnionRepo.Insert(entity);
            unitOfWork.Save();
        }
        public void AddPartners(int id, int img_id, string name, string about)
        {
            unitOfWork.PartnersRepo.Insert(new Partners
            {
                Image_Id = img_id,
                AboutUnion_Id = id,
                ParnerAbout = about,
                ParnerName = name
            });
            unitOfWork.Save();

        }
        public void AddCrew(int id, int img_id, string name, string about)
        {
            unitOfWork.PartnersRepo.Insert(new Partners
            {
                Image_Id = img_id,
                AboutUnion_Id = id,
                ParnerAbout = about,
                ParnerName = name
            });
            unitOfWork.Save();
        }
        public void Update(AboutUnion entity)
        {
            var aboutUn = unitOfWork.AboutUnionRepo.GetById(1);
            aboutUn.Mission = entity.Mission;
            aboutUn.Union = entity.Union;
            aboutUn.Goals = entity.Goals;
            unitOfWork.Save();
        }
        public void Delete(AboutUnion entityToDelete)
        {
            unitOfWork.AboutUnionRepo.Delete(entityToDelete);
            unitOfWork.Save();
        }



    }
}

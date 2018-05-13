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
    public class PersonManager : IPersonManager
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public PersonManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<Person> GetAll()
        {
            return unitOfWork.PersonRepo.GetAll();
        }

        public Person GetById(int id)
        {
            return mapper.Map<Person>(unitOfWork.PersonRepo.GetById(id));
        }

        public virtual IEnumerable<Person> Get(
            Expression<Func<Person, bool>> filter = null,
            Func<IQueryable<Person>,
            IOrderedQueryable<Person>> orderBy = null,
            string includeProperties = "")
        {
            return unitOfWork.PersonRepo.Get(filter, orderBy, includeProperties);
        }

        public void Insert(Person entity)
        {
            unitOfWork.PersonRepo.Insert(entity);
            unitOfWork.Save();
        }

        public void Update(Person entityToUpdate)
        {
            unitOfWork.PersonRepo.Update(entityToUpdate);
            unitOfWork.Save();
        }


        public void Delete(Person entityToDelete)
        {
            unitOfWork.PersonRepo.Delete(entityToDelete);
            unitOfWork.Save();
        }
    }
}

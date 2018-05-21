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
    public class MessagesManager : IMessagesManager
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public MessagesManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<Messages> GetAll()
        {
            return unitOfWork.MessagesRepo.GetAll();
        }

        public Messages GetById(int id)
        {
            return unitOfWork.MessagesRepo.GetById(id);
        }

        public virtual IEnumerable<Messages> Get(Expression<Func<Messages, bool>> filter = null,
                                     Func<IQueryable<Messages>,
                                     IOrderedQueryable<Messages>> orderBy = null,
                                     string includeProperties = "")
        {
            return unitOfWork.MessagesRepo.Get(filter, orderBy, includeProperties);
        }

        public void Insert(Messages entity)
        {
            unitOfWork.MessagesRepo.Insert(entity);
            unitOfWork.Save();
        }
        public void Update(Messages entity)
        {
            unitOfWork.MessagesRepo.Update(entity);
            unitOfWork.Save();
        }

        public void DeleteOrRecover(int id)
        {
            var message = unitOfWork.MessagesRepo.GetById(id);
            message.IsDeleted = !message.IsDeleted;
            unitOfWork.MessagesRepo.Update(message);
            unitOfWork.Save();
        }

        public void Delete(Messages entityToDelete)
        {
            unitOfWork.MessagesRepo.Delete(entityToDelete);
            unitOfWork.Save();
        }
    }
}

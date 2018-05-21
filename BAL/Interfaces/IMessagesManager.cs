using Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BAL.Interfaces
{
    public interface IMessagesManager
    {
        void Delete(Messages entityToDelete);
        void DeleteOrRecover(int id);
        IEnumerable<Messages> Get(Expression<Func<Messages, bool>> filter = null, Func<IQueryable<Messages>, IOrderedQueryable<Messages>> orderBy = null, string includeProperties = "");
        IEnumerable<Messages> GetAll();
        Messages GetById(int id);
        void Insert(Messages entity);
        void Update(Messages entity);
    }
}

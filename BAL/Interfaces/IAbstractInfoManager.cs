using Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BAL.Interfaces
{
    public interface IAbstractInfoManager
    {
        IEnumerable<AbstractInfo> GetAll();

        AbstractInfo GetById(int id);

        IEnumerable<AbstractInfo> Get(Expression<Func<AbstractInfo, bool>> filter = null,
                                     Func<IQueryable<AbstractInfo>,
                                     IOrderedQueryable<AbstractInfo>> orderBy = null,
                                     string includeProperties = "");

        void Insert(AbstractInfo entity);

        void Update(AbstractInfo entity);
        void Delete(AbstractInfo entityToDelete);
    }
}

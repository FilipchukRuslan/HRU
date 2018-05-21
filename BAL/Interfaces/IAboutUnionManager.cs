using Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BAL.Interfaces
{
    public interface IAboutUnionManager
    {
        IEnumerable<AboutUnion> GetAll();

        AboutUnion GetById(int id);

        IEnumerable<AboutUnion> Get(Expression<Func<AboutUnion, bool>> filter = null,
                                     Func<IQueryable<AboutUnion>,
                                     IOrderedQueryable<AboutUnion>> orderBy = null,
                                     string includeProperties = "");

        void Insert(AboutUnion entity);

        void Update(AboutUnion entity);

        void Delete(AboutUnion entityToDelete);
    }
}

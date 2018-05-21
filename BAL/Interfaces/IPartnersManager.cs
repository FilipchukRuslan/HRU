using Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BAL.Interfaces
{
    public interface IPartnersManager
    {
        IEnumerable<Partners> GetAll();

        Partners GetById(int id);

        IEnumerable<Partners> Get(Expression<Func<Partners, bool>> filter = null,
                                     Func<IQueryable<Partners>,
                                     IOrderedQueryable<Partners>> orderBy = null,
                                     string includeProperties = "");

        void Insert(Partners entity);

        void Update(Partners entity);

        void Delete(Partners entityToDelete);
    }
}

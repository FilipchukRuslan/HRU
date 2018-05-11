using Model.DB;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BAL.Interfaces
{
    public interface IProjectsManager
    {
        IEnumerable<Projects> GetAll();
        IEnumerable<Projects> Get(
           Expression<Func<Projects, bool>> filter = null,
           Func<IQueryable<Projects>,
           IOrderedQueryable<Projects>> orderBy = null,
           string includeProperties = "");
        Projects GetById(int id);
        void Insert(Projects item);
        void Update(Projects item);
        void Delete(Projects item);
    }
}

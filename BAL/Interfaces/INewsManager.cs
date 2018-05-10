using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Model.DB;
using Model.Entity;

namespace BAL.Interfaces
{
    public interface INewsManager
    {
        IEnumerable<News> GetAll();

        News GetById(int id);

        IEnumerable<News> Get(Expression<Func<News, bool>> filter = null,
                                     Func<IQueryable<News>,
                                     IOrderedQueryable<News>> orderBy = null,
                                     string includeProperties = "");

        void Insert(News entity);

        void Update(News entity);

        void DeleteOrRecover(int id);
    }
}

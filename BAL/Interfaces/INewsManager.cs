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
        IEnumerable<NewsDTO> GetAll();
        IEnumerable<NewsDTO> Get(
           Expression<Func<News, bool>> filter = null,
           Func<IQueryable<News>,
           IOrderedQueryable<News>> orderBy = null,
           string includeProperties = "");
        NewsDTO GetById(int id);
        void Insert(NewsDTO item);
        void Update(NewsDTO item);
        void Delete(NewsDTO item);
    }
}

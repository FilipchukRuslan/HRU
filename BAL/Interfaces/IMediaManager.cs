using Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BAL.Interfaces
{
    public interface IMediaManager
    {
        IEnumerable<Media> GetAll();

        Media GetById(int id);

        IEnumerable<Media> Get(Expression<Func<Media, bool>> filter = null,
                                     Func<IQueryable<Media>,
                                     IOrderedQueryable<Media>> orderBy = null,
                                     string includeProperties = "");

        void Insert(Media entity);

        void Update(Media entity);
        void Delete(Media entityToDelete);
    }
}

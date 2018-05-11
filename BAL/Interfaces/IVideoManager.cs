using Model.DB;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BAL.Interfaces
{
    public interface IVideoManager
    {
        IEnumerable<Video> GetAll();
        IEnumerable<Video> Get(
           Expression<Func<Video, bool>> filter = null,
           Func<IQueryable<Video>,
           IOrderedQueryable<Video>> orderBy = null,
           string includeProperties = "");
        Video GetById(int id);
        void Insert(Video item);
        void Update(Video item);
        void Delete(Video item);
    }
}

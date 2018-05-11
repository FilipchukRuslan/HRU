using Model.DB;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BAL.Interfaces
{
    public interface IFaceBookManager
    {
        IEnumerable<FaceBook> GetAll();
        IEnumerable<FaceBook> Get(
           Expression<Func<FaceBook, bool>> filter = null,
           Func<IQueryable<FaceBook>,
           IOrderedQueryable<FaceBook>> orderBy = null,
           string includeProperties = "");
        FaceBook GetById(int id);
        void Insert(FaceBook item);
        void Update(FaceBook item);
        void Delete(FaceBook item);
    }
}

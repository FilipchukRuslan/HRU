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
        IEnumerable<FaceBookDTO> GetAll();
        IEnumerable<FaceBookDTO> Get(
           Expression<Func<FaceBook, bool>> filter = null,
           Func<IQueryable<FaceBook>,
           IOrderedQueryable<FaceBook>> orderBy = null,
           string includeProperties = "");
        FaceBookDTO GetById(int id);
        void Insert(FaceBookDTO item);
        void Update(FaceBookDTO item);
        void Delete(FaceBookDTO item);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Model.DB;
using Model.DTO;
using System.Linq;
using System.Linq.Expressions;

namespace BAL.Interfaces
{
    public  interface IImageManager
    {
        IEnumerable<Image> GetAll();
        IEnumerable<Image> Get(
           Expression<Func<Image, bool>> filter = null,
           Func<IQueryable<Image>,
           IOrderedQueryable<Image>> orderBy = null,
           string includeProperties = "");
        Image GetById(int id);
        void Insert(Image item);
        void Update(Image item);
        void Delete(Image item);
    }
}

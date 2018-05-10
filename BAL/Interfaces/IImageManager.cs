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
        IEnumerable<ImageDTO> GetAll();
        IEnumerable<ImageDTO> Get(
           Expression<Func<Image, bool>> filter = null,
           Func<IQueryable<Image>,
           IOrderedQueryable<Image>> orderBy = null,
           string includeProperties = "");
        ImageDTO GetById(int id);
        void Insert(ImageDTO item);
        void Update(ImageDTO item);
        void Delete(ImageDTO item);
    }
}

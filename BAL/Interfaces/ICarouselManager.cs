using System;
using System.Collections.Generic;
using System.Text;
using Model.DB;
using Model.DTO;
using System.Linq;
using System.Linq.Expressions;


namespace BAL.Interfaces
{
    public interface ICarouselManager
    {
        IEnumerable<Carousel> GetAll();
        IEnumerable<Carousel> Get(
           Expression<Func<Carousel, bool>> filter = null,
           Func<IQueryable<Carousel>,
           IOrderedQueryable<Carousel>> orderBy = null,
           string includeProperties = "");
        Carousel GetById(int id);
        void Insert(Carousel item);
        void Update(Carousel item);
        void Delete(Carousel item);
    }
}

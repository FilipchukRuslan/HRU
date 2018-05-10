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
        IEnumerable<CarouselDTO> GetAll();
        IEnumerable<CarouselDTO> Get(
           Expression<Func<Carousel, bool>> filter = null,
           Func<IQueryable<Carousel>,
           IOrderedQueryable<Carousel>> orderBy = null,
           string includeProperties = "");
        CarouselDTO GetById(int id);
        void Insert(CarouselDTO item);
        void Update(CarouselDTO item);
        void Delete(CarouselDTO item);
    }
}

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
        IEnumerable<VideoDTO> GetAll();
        IEnumerable<VideoDTO> Get(
           Expression<Func<Video, bool>> filter = null,
           Func<IQueryable<Video>,
           IOrderedQueryable<Video>> orderBy = null,
           string includeProperties = "");
        VideoDTO GetById(int id);
        void Insert(VideoDTO item);
        void Update(VideoDTO item);
        void Delete(VideoDTO item);
    }
}

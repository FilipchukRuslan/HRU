using Model.DB;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BAL.Interfaces
{
    public interface IProjectsManager
    {
        IEnumerable<ProjectsDTO> GetAll();
        IEnumerable<ProjectsDTO> Get(
           Expression<Func<Projects, bool>> filter = null,
           Func<IQueryable<Projects>,
           IOrderedQueryable<Projects>> orderBy = null,
           string includeProperties = "");
        ProjectsDTO GetById(int id);
        void Insert(ProjectsDTO item);
        void Update(ProjectsDTO item);
        void Delete(ProjectsDTO item);
    }
}

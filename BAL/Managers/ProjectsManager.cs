using Microsoft.CodeAnalysis;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System.Diagnostics;
using Model.Entity;
using BAL.Interfaces;
using System;
using System.IO;
using DAL.Interface;
using AutoMapper;
using Model.DB;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;

namespace BAL.Managers
{
    public class ProjectsManager : IProjectsManager
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public ProjectsManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Projects> GetAll()
        {
            return unitOfWork.ProjectsRepo.GetAll();
        }

        public Projects GetById(int id)
        {
            return unitOfWork.ProjectsRepo.GetById(id);
        }

        public virtual IEnumerable<Projects> Get(
            Expression<Func<Projects, bool>> filter = null,
            Func<IQueryable<Projects>,
            IOrderedQueryable<Projects>> orderBy = null,
            string includeProperties = "")
        {
            return unitOfWork.ProjectsRepo.Get(filter, orderBy, includeProperties);
        }

        public void Insert(Projects entity)
        {
            unitOfWork.ProjectsRepo.Insert(entity);
            unitOfWork.Save();
        }

        public void Update(Projects entityToUpdate)
        {
            unitOfWork.ProjectsRepo.Update(entityToUpdate);
            unitOfWork.Save();
        }



        public void Delete(Projects entityToDelete)
        {
            unitOfWork.ProjectsRepo.Delete(entityToDelete);
            unitOfWork.Save();
        }
    }
}

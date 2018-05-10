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

        public IEnumerable<ProjectsDTO> GetAll()
        {
            return mapper.Map<List<ProjectsDTO>>(unitOfWork.ProjectsRepo.GetAll());
        }

        public ProjectsDTO GetById(int id)
        {
            return mapper.Map<ProjectsDTO>(unitOfWork.ProjectsRepo.GetById(id));
        }

        public virtual IEnumerable<ProjectsDTO> Get(
            Expression<Func<Projects, bool>> filter = null,
            Func<IQueryable<Projects>,
            IOrderedQueryable<Projects>> orderBy = null,
            string includeProperties = "")
        {
            return mapper.Map<List<ProjectsDTO>>(unitOfWork.ProjectsRepo.Get(filter, orderBy, includeProperties));
        }

        public void Insert(ProjectsDTO entity)
        {
            unitOfWork.ProjectsRepo.Insert(mapper.Map<Projects>(entity));
            unitOfWork.Save();
        }

        public void Update(ProjectsDTO entityToUpdate)
        {
            unitOfWork.ProjectsRepo.Update(mapper.Map<Projects>(entityToUpdate));
            unitOfWork.Save();
        }



        public void Delete(ProjectsDTO entityToDelete)
        {
            unitOfWork.ProjectsRepo.Delete(mapper.Map<Projects>(entityToDelete));
            unitOfWork.Save();
        }
    }
}

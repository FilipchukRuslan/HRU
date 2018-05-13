using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Model.DB;

namespace BAL.Interfaces
{
    public interface IPersonManager
    {
        void Delete(Person entityToDelete);
        IEnumerable<Person> Get(Expression<Func<Person, bool>> filter = null, Func<IQueryable<Person>, IOrderedQueryable<Person>> orderBy = null, string includeProperties = "");
        IEnumerable<Person> GetAll();
        Person GetById(int id);
        void Insert(Person entity);
        void Update(Person entityToUpdate);
    }
}
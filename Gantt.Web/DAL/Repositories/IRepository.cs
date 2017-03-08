using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gantt.Web.DAL
{
    interface IRepository<T>
    {
        void Insert(T entity);
        void Delete(int id);
        void Update(T entity);
        IEnumerable<T> SearchFor(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        T GetById(int id);
    }
}

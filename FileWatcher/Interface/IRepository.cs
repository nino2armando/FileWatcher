using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcher.Interface
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        IQueryable<T> All();
        T GetById(object id);
    }
}

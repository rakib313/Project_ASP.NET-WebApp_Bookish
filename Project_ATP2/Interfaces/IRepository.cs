using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ATP2.Interfaces
{
    interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();

        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Modify(TEntity entity);

        void Save();

    }
}

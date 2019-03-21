using MyEverNote.DataAccessLayer;
using MyEverNote.DataAccessLayer.Abstract;
using MyEverNote.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccessLayer.EntityFramework
{
    public class Repository<T> : RepositoryBase, IRepository<T> where T : class
    {
        //private DatabaseContext db;
        private DbSet<T> _objectSet;

        public Repository()
        {
            _objectSet = context.Set<T>();
        }

        public List<T> List()
        {
            return _objectSet.ToList();
        }

        public List<T> List(Expression<Func<T,bool>> where)
        {
            return _objectSet.Where(where).ToList();
        }

        public int Insert(T obj)
        {
            _objectSet.Add(obj);
            return Save();
        }

        public int Update(T obj)
        {
            return Save();
        }

        public int Delete(T obj)
        {
            _objectSet.Remove(obj);
            return Save();
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public T  Find(Expression<Func<T, bool>> where)
        {
            return _objectSet.FirstOrDefault(where);
        }

    }
}

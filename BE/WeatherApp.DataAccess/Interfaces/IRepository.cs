using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeatherApp.DataAccess.Interfaces
{
    public interface IRepository<T>
    {
        void Update(T entity);
        T GetById(int id);
        IQueryable<T> GetAll();
        void Add(T entity);
        void Delete(T entity);
        void SaveChanges();
    }
}

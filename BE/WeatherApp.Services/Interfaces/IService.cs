using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Services.Interfaces
{
    public interface IService<T>
    {
        T AddEntity(T entity);
        void DeleteEntity(int id);

        List<T> GetAllEntities();

        T GetEntityById(int id);

        void UpdateEntity(T entity);
    }
}

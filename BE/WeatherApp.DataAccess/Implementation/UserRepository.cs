using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeatherApp.DataAccess.Interfaces;
using WeatherApp.Domain.Models;

namespace WeatherApp.DataAccess.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly WeatherAppDbContext _weatherAppDbContext;
        public UserRepository(WeatherAppDbContext weatherAppDbContext)
        {
            _weatherAppDbContext = weatherAppDbContext;
        }
        public void Add(User entity)
        {
            _weatherAppDbContext.Users.Add(entity);

        }

        public void Delete(User entity)
        {
            _weatherAppDbContext.Users.Remove(entity);

        }

        public IQueryable<User> GetAll()
        {
            return _weatherAppDbContext
                .Users
                .AsQueryable();
        }

        public User GetById(int id)
        {
            return _weatherAppDbContext
                .Users
                .FirstOrDefault(x => x.Id == id);
        }

        public User GetByUsername(string username)
        {
            return _weatherAppDbContext
                .Users
                .FirstOrDefault(x => x.Email == username);
        }

        public User Login(string username, string password)
        {
            return _weatherAppDbContext
                .Users
                .FirstOrDefault(x => x.Email == username && x.Password == password);
        }

        public void SaveChanges()
        {
            _weatherAppDbContext.SaveChanges();
        }

        public void Update(User entity)
        {
            _weatherAppDbContext.Users.Update(entity);
        }
    }
}

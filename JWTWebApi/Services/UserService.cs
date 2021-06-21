using JWTWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTWebApi.Services
{
    public interface IUserService
    {
        User Authenticate(Login model);
        IEnumerable<User> GetAll();
        User GetById(int id);
        User GetByName(string firstName, string lastname);
        User Create(User user, string password);
        void Update(User user, string password = null);
        void Delete(int id);
    }
    public class UserService: IUserService
    {
        private SalonApiContext _context;

        public UserService(SalonApiContext context)
        {
            _context = context;
        }

        public User Authenticate(Login model)
        {
            if (string.IsNullOrEmpty(model.username) || string.IsNullOrEmpty(model.password))
                return null;

            var user = _context.Users.SingleOrDefault(x => x.username == model.username && x.password == model.password);

            if (user == null)
                return null;

            return user;
        }

        public User Create(User user, string password)
        {
            if(string.IsNullOrWhiteSpace(password)) {
                throw new AppException("Password is required");
            }

            if(_context.Users.Any(x => x.username == user.username))
            {
                throw new AppException("Username \"" + user.username + "\" is already taken");
            }

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
            
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if(user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public User GetByName(string firstName, string lastName)
        {
            return _context.Users.Where(x => x.firstName == firstName && x.lastName == lastName).FirstOrDefault();
        }

        public void Update(User userParam, string password)
        {
            var user = _context.Users.Find(userParam.id);

            if (user == null)
                throw new AppException("User not found");

            _context.Users.Update(user);
            _context.SaveChanges();

        }
    }
}

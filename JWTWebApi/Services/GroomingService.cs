using JWTWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTWebApi.Services
{
    public interface IGroomingService
    {
        IEnumerable<Grooming> GetAll();
        Grooming GetById(int id);
        Grooming Create(Grooming model);
        void Update(int id, Grooming model);
        void Delete(int id);
    }
    public class GroomingService : IGroomingService
    {
        private SalonApiContext _context;

        public GroomingService(SalonApiContext context)
        {
            _context = context;
        }
        public Grooming Create(Grooming model)
        {
            _context.Groomings.Add(model);
            _context.SaveChanges();

            return model;
        }

        public void Delete(int id)
        {
            var model = _context.Groomings.Find(id);
            if (model != null)
            {
                _context.Groomings.Remove(model);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Grooming> GetAll()
        {
            return _context.Groomings;
        }

        public Grooming GetById(int id)
        {
            return _context.Groomings.Find(id);
        }

        public void Update(int id, Grooming model)
        {
            var groom = _context.Groomings.Find(id);
            if (groom != null)
            {
                _context.Groomings.Update(model);
                _context.SaveChanges();
            }
        }
    }
}

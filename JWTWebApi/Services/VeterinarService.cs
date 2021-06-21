using JWTWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTWebApi.Services
{
    public interface IVeterinarService
    {
        IEnumerable<Veterinar> GetAll();
        Veterinar GetById(int id);
        Veterinar Create(Veterinar model);
        void Update(int id, Veterinar model);
        void Delete(int id);
    }
    public class VeterinarService : IVeterinarService
    {
        private SalonApiContext _context;

        public VeterinarService(SalonApiContext context)
        {
            _context = context;
        }
        public Veterinar Create(Veterinar model)
        {
            _context.Veterinars.Add(model);
            _context.SaveChanges();

            return model;
        }

        public void Delete(int id)
        {
            var model = _context.Veterinars.Find(id);
            if (model != null)
            {
                _context.Veterinars.Remove(model);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Veterinar> GetAll()
        {
            return _context.Veterinars;
        }

        public Veterinar GetById(int id)
        {
            return _context.Veterinars.Find(id);
        }

        public void Update(int id, Veterinar model)
        {
            var vet = _context.Veterinars.Find(id);
            if (vet != null)
            {
                _context.Veterinars.Update(model);
                _context.SaveChanges();
            }
        }
    }
}

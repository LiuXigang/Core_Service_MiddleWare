using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_Service_MiddleWare.Data;
using Core_Service_MiddleWare.Models;

namespace Core_Service_MiddleWare.Service
{
    public class EFCoreRepository : IRepository<Student>
    {
        private readonly DataContext _context;
        public EFCoreRepository(DataContext context)
        {
            _context = context;
        }
        public void Add(Student t)
        {
            _context.Student.Add(t);
            _context.SaveChanges();
        }

        public IEnumerable<Student> GetAll()
        {
            return _context.Student.ToList();
        }

        public Student GetById(int id)
        {
            return _context.Student.FirstOrDefault(n => n.Id == id);
        }
    }
}

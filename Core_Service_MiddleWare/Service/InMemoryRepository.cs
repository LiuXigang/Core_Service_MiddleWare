using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_Service_MiddleWare.Models;

namespace Core_Service_MiddleWare.Service
{
    public class InMemoryRepository : IRepository<Student>
    {
        private readonly List<Student> _students;
        public InMemoryRepository()
        {
            _students = new List<Student> {
                new Student{Id=1,Age=1,Name="Tom"},new Student{Id=2,Age=2,Name="Jim"},new Student{Id=1,Age=3,Name="Dave"}
            };
        }

        public void Add(Student t)
        {
            _students.Add(t);
        }

        public IEnumerable<Student> GetAll()
        {
            return _students;
        }

        public Student GetById(int id)
        {
            return _students.FirstOrDefault(n => n.Id == id);
        }
    }
}

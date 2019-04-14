using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_Service_MiddleWare.Models;

namespace Core_Service_MiddleWare.Service
{
    public class InMemoryRepository : IRepository<Student>
    {
        public IEnumerable<Student> GetAll()
        {
            return new List<Student> {
                new Student{Age=1,Name="Tom"},new Student{Age=2,Name="Jim"},new Student{Age=3,Name="Dave"}
            };
        }
    }
}

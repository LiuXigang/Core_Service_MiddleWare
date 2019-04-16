using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_Service_MiddleWare.Models;
using Microsoft.EntityFrameworkCore;

namespace Core_Service_MiddleWare.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Student> Student { get; set; }
    }
}

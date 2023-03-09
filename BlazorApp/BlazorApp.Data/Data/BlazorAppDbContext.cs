using BlazorApp.Data.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Data.Data
{
    public class BlazorAppDbContext : DbContext
    {
        //public BlazorAppDbContext(DbContextOptions<BlazorAppDbContext> dbContextOptions)
        //{

        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "EmployeeDb");
        }
        public DbSet<Employee> Employees { get; set; }
    }
}

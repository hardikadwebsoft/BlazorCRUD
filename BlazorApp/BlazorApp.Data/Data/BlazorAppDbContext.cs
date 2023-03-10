﻿using BlazorApp.Data.DataModels;
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
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            //seed Employee Data
            modelBuilder.Entity<Employee>().HasData(
                new Employee() { Id = 1, Name = "John", Email = "johndoe@example.com", DateOfBirth = "2 jan 1986", Department = "Testing" },
                new Employee() { Id = 2, Name = "Jack", Email = "jack@example.com", DateOfBirth = "2 jan 1986", Department = "Marketing" },
                new Employee() { Id = 3, Name = "Sam", Email = "dank@example.com", DateOfBirth = "4 jan 1987", Department = "Testing" },
                new Employee() { Id = 4, Name = "Paul", Email = "paul@example.com", DateOfBirth = "5 jan 1989", Department = "Support" }
           );
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "EmployeeDb");
        }

    }
}

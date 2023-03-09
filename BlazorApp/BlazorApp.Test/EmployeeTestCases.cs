using AutoMapper;
using BlazorApp.Data.Data;
using BlazorApp.Data.DataModels;
using BlazorApp.Models.ViewModels;
using BlazorApp.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlazorApp.Test
{
    public class EmployeeTestCases
    {
        
        BlazorAppDbContext context;
        IMapper mapper;

        [OneTimeSetUp]
        public void Setup()
        {

             DbContextOptions<BlazorAppDbContext> dbContextOptions = new DbContextOptionsBuilder<BlazorAppDbContext>()
            .UseInMemoryDatabase(databaseName: "BlazorAppTest").Options;

             context = new BlazorAppDbContext();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Employee, EmployeeViewModel>();
                //.ForMember(dest => dest.Id, opt => opt.Ignore());
                cfg.CreateMap<EmployeeViewModel, Employee>();
                //.ForMember(dest => dest.Id, opt => opt.Ignore());
            });
            mapper = config.CreateMapper();
            context.Database.EnsureCreated();
            SeedDatabase();
        }
        #region Seed Data
        private void SeedDatabase()
        {
            context.Employees.AddRange(
                new Employee() { Id = 1, Name = "John", Email = "johndoe@example.com", DateOfBirth = "2 jan 1986", Department = "IT" },
                new Employee() { Id = 2, Name = "Jack", Email = "jack@example.com", DateOfBirth = "2 jan 1986", Department = "IT" }
            );

            context.SaveChanges();
        }
        #endregion

        [Test]
        public async Task Should_Create_Employee()
        {
            // Act
            context.Database.EnsureDeleted();
            context = new BlazorAppDbContext();
            var result = await context.Employees.CountAsync();

            // Arrange
            EmployeeRepository employeeRepository = new EmployeeRepository(context, mapper);
            var employee = new EmployeeViewModel()
            {
                Id = 1,
                Name = "John",
                Email = "johndoe@example.com",
                DateOfBirth = "5 feb 2020",
                Department = "IT",
            };
            await employeeRepository.Create(employee);

            //Assert
            Assert.AreEqual(1, await context.Employees.CountAsync());
        }

        [Test]
        public async Task Should_List_All_Employees()
        {
            // Act
            context.Database.EnsureDeleted();
            context = new BlazorAppDbContext();
            SeedDatabase();
            EmployeeRepository employeeRepository = new EmployeeRepository(context, mapper);
            var allEmployees = await employeeRepository.GetList();

            // Assert
            Assert.AreEqual(2, allEmployees.Count);

            Assert.AreEqual("John", allEmployees.First().Name);
            Assert.AreEqual("johndoe@example.com", allEmployees.First().Email);
            Assert.AreEqual("2 jan 1986", allEmployees.First().DateOfBirth);
            Assert.AreEqual("IT", allEmployees.First().Department);


            Assert.AreEqual("Jack", allEmployees.Last().Name);
            Assert.AreEqual("jack@example.com", allEmployees.Last().Email);
            Assert.AreEqual("2 jan 1986", allEmployees.Last().DateOfBirth);
            Assert.AreEqual("IT", allEmployees.Last().Department);

        }

        [Test]
        public async Task Should_Search_All_Employees()
        {
            // Act
            context.Database.EnsureDeleted();
            context = new BlazorAppDbContext();
            SeedDatabase();
            EmployeeRepository employeeRepository = new EmployeeRepository(context, mapper);
            var allEmployees = await employeeRepository.GetList();

            // Act
            var results = await employeeRepository.Search("Jack");

            // Assert 
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Jack",results.Select(x=>x.Name).FirstOrDefault().ToString());
            
        }

    }
}

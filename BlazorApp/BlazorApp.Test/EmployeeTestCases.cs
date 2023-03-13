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
                cfg.CreateMap<EmployeeViewModel, Employee>();
            });
            mapper = config.CreateMapper();
            context.Database.EnsureCreated();
            SeedDatabase();
        }
        #region Seed Data
        private void SeedDatabase()
        {
            //seed Department Data

            context.Departments.AddRange(
                new Department() { Id = 111, Name = "Marketing" },
                new Department() { Id = 112, Name = "IT" },
                new Department() { Id = 113, Name = "Support" },
                new Department() { Id = 114, Name = "Testing" },
                new Department() { Id = 115, Name = "HR" }

           );

            context.Employees.AddRange(
              new Employee() { Id = 5, Name = "John", Email = "johndoe@example.com", DateOfBirth = "2 jan 1986", DepartmentId = 111 },
              new Employee() { Id = 6, Name = "Jack", Email = "jack@example.com", DateOfBirth = "2 jan 1986", DepartmentId =112 },
              new Employee() { Id = 7, Name = "Bob", Email = "bob@example.com", DateOfBirth = "2 jan 1986", DepartmentId = 113 }

          );

            context.SaveChanges();
        }
        #endregion

        [Test]
        public async Task Should_Create_Employee()
        {
            // Arrange
            EmployeeRepository employeeRepository = new EmployeeRepository(context, mapper);
            var result = await employeeRepository.GetList();
            var employee = new EmployeeViewModel()
            {
                Id = 8,
                Name = "Mack",
                Email = "mack@example.com",
                DateOfBirth = "5 feb 2020",
                DepartmentId = 1,

            };

            await employeeRepository.Create(employee);
            var resultAfterAdd = await employeeRepository.GetList();
            //Assert
            Assert.Greater(resultAfterAdd.Count, result.Count);
        }
        [Test]
        public async Task Should_Update_Employee()
        {
            // Arrange
            EmployeeRepository employeeRepository = new EmployeeRepository(context, mapper);
            var employeee = await employeeRepository.GetById(6);
           ;

            // Act
            employeee.Email = "jackscott@example.com";

           
            await employeeRepository.Update(employeee);

            // Assert
            var updatedEmployee = await employeeRepository.GetById(employeee.Id);
            Assert.NotNull(updatedEmployee);
            Assert.AreEqual("jackscott@example.com", updatedEmployee.Email);

        }

        [Test]
        public async Task Should_Delete_Employee()
        {
            // Arrange
            EmployeeRepository employeeRepository = new EmployeeRepository(context, mapper);
            var result = employeeRepository.GetList();
            // Act
            await employeeRepository.Delete(7);
            // Assert
            var deletedEmployee = await employeeRepository.GetById(7);
            Assert.Null(deletedEmployee);
        }
       
        [Test]
        public async Task Should_List_All_Employees()
        {
            EmployeeRepository employeeRepository = new EmployeeRepository(context, mapper);
            var allEmployees = await employeeRepository.GetList();
            // Assert
            Assert.GreaterOrEqual(allEmployees.Count, 1);
        }

        [Test]
        public async Task Should_Search_All_Employees()
        {
            
           //Arrange
            EmployeeRepository employeeRepository = new EmployeeRepository(context, mapper);
            var allEmployees = await employeeRepository.GetList();
            // Act
            var results = await employeeRepository.Search("Jack");
            // Assert 
            Assert.GreaterOrEqual(results.Count,1);
            Assert.AreEqual("Jack", results.Select(x => x.Name).FirstOrDefault().ToString());

        }
        [Test]
        public async Task Should_Search_With_Department()
        {
            //Arrange
            EmployeeRepository employeeRepository = new EmployeeRepository(context, mapper);
            var allEmployees = await employeeRepository.GetList();
            //Act
            var result = await employeeRepository.Search("Marketing");
            //Assert
            Assert.GreaterOrEqual(result.Count,1);

        }


    }
}

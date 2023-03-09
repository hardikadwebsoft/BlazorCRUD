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
    public class RepositoryTestCases
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
        public async Task Task_Add_ValidData_Comparetotal()
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
        public async Task Task_Add_InValidData_Comparetotal()
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
                //Email = "johndoe@example.com",
                DateOfBirth = "5 feb 2020",
                Department = "IT",
            };

            // Act and Assert
            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => employeeRepository.Create(employee));
            Assert.That(ex.Message, Is.EqualTo("Value cannot be null. (Parameter 'employeeViewModel')"));

        }

        [Test]
        public async Task UpdateEmployee_WithValidData_ShouldUpdateEmployee()
        {
            // Arrange
            EmployeeRepository employeeRepository = new EmployeeRepository(context, mapper);
            var employeUpdate = await employeeRepository.GetById(2);
            // Act
            employeUpdate.Name = "Jane";
            employeUpdate.Department = "HR";

            await employeeRepository.Update(employeUpdate,true);

            // Assert
            var updatedEmployee = await employeeRepository.GetById(employeUpdate.Id);
            Assert.NotNull(updatedEmployee);
            Assert.AreEqual("Jane", updatedEmployee.Name);
            Assert.AreEqual("HR", updatedEmployee.Department);
        }


        [Test]
        public async Task GetAllEmployees_ReturnsAllEmployeesInDatabase()
        {
            // Act
            context.Database.EnsureDeleted();
            context = new BlazorAppDbContext();
            SeedDatabase();
            EmployeeRepository employeeRepository = new EmployeeRepository(context, mapper);
            var allEmployees = await employeeRepository.GetList();
            // Assert
            Assert.AreEqual(2, allEmployees.Count);

            Assert.AreEqual("John", allEmployees[0].Name);
            Assert.AreEqual("johndoe@example.com", allEmployees[0].Email);
            Assert.AreEqual("2 jan 1986", allEmployees[0].DateOfBirth);
            Assert.AreEqual("IT", allEmployees[0].Department);


            Assert.AreEqual("Jack", allEmployees[1].Name);
            Assert.AreEqual("jack@example.com", allEmployees[1].Email);
            Assert.AreEqual("2 jan 1986", allEmployees[1].DateOfBirth);
            Assert.AreEqual("IT", allEmployees[1].Department);

        }

        [Test]
        public async Task SearchEmployees_ReturnsMatchingEmployees()
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

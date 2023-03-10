using AutoMapper;
using BlazorApp.Data.Data;
using BlazorApp.Data.DataModels;
using BlazorApp.Helper;
using BlazorApp.Models.Models;
using BlazorApp.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;

namespace BlazorApp.Service
{
    public interface IEmployeeRepository
    {
        //GetEmployees
        Task<List<EmployeeViewModel>> GetList();

        //GetEmployeeById
        Task<EmployeeViewModel> GetById(int id);

        //PostEmployee
        Task Create(EmployeeViewModel employeeViewModel);
        //UpdateEmployee
        Task Update(EmployeeViewModel employeeViewModel);
        //DeleteEmployee
        Task Delete(int id);

        //Search
        Task<List<EmployeeViewModel>> Search(string searchString);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly BlazorAppDbContext _context;
        private readonly IMapper _mapper;
        public EmployeeRepository(BlazorAppDbContext context,IMapper mapper)
        {
            _context = context;
            _context.Database.EnsureCreated();
            _mapper = mapper;
        }

        public async Task<List<EmployeeViewModel>> GetList()
        {
            
            var results = new List<EmployeeViewModel>();
            try
            {
                var employees = await _context.Employees.AsNoTracking().ToListAsync();
                results = _mapper.Map<List<EmployeeViewModel>>(employees);
                return results;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<EmployeeViewModel> GetById(int id)
        {
            try
            {
                var employee =await  _context.Employees.AsNoTracking().FirstOrDefaultAsync(x=>x.Id == id);
                          
                var result = _mapper.Map<EmployeeViewModel>(employee);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task Create(EmployeeViewModel employeeViewModel)
        {
            try
            {
                var data = _mapper.Map<Employee>(employeeViewModel);
                await _context.Employees.AddAsync(data);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task Update(EmployeeViewModel employeeViewModel)
        {
            try
            {
                var employee = _mapper.Map<Employee>(employeeViewModel);
                _context.ChangeTracker.Clear();
                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task Delete(int id)
        {
            try
            {
                var dbEmployee = await _context.Employees.FindAsync(id);
                if (dbEmployee == null)
                {
                    throw new Exception("Employee Not Found");
                }
                _context.Employees.Remove(dbEmployee);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //Search
        public async Task<List<EmployeeViewModel>> Search(string searchString)
        {
            var results = new List<EmployeeViewModel>();
            try
            {
                var stringcomparison = StringComparison.InvariantCultureIgnoreCase;
              
                var employees = await _context.Employees.Where(x=>x.Name.Contains(searchString, stringcomparison) || x.Email.Contains(searchString, stringcomparison) || x.Department.Contains(searchString, stringcomparison)).ToListAsync();
                results = _mapper.Map<List<EmployeeViewModel>>(employees);
                return results;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

       

    }

   
}

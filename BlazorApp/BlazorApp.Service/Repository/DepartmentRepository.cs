using AutoMapper;
using BlazorApp.Data.Data;
using BlazorApp.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Service.Repository
{
    public interface IDepartmentRepository
    {
        //GetDepartments
        Task<List<DepartmentViewModel>> GetList();

        //GetEmployeeById
        Task<DepartmentViewModel> GetById(int id);
    }

    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly BlazorAppDbContext _context;
        private readonly IMapper _mapper;
        public DepartmentRepository(BlazorAppDbContext context , IMapper mapper)
        {
            _context = context;
            _context.Database.EnsureCreated();
            _mapper = mapper;
        }

        public async Task<List<DepartmentViewModel>> GetList()
        {

            var results = new List<DepartmentViewModel>();
            try
            {
                var departments = await _context.Departments.AsNoTracking().ToListAsync();
                results = _mapper.Map<List<DepartmentViewModel>>(departments);
                return results;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<DepartmentViewModel> GetById(int id)
        {
            try
            {
                var department = await _context.Departments.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

                var result = _mapper.Map<DepartmentViewModel>(department);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

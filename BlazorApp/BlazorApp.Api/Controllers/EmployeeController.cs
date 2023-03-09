using AutoMapper;
using BlazorApp.Data.Data;
using BlazorApp.Data.DataModels;
using BlazorApp.Models.Models;
using BlazorApp.Models.ViewModels;
using BlazorApp.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BlazorApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        [HttpGet]
        [Route("GetEmployeeList")]
        public async Task<List<EmployeeViewModel>> GetEmployeeList()
        {
            var empList = await _employeeRepository.GetList();
            return empList;
        }

        [Route("PostEmployee")]
        [HttpPost]
        public async Task<EmployeeViewModel> PostEmployee(EmployeeViewModel employeeViewModel)
        {
            await _employeeRepository.Create(employeeViewModel);
            return employeeViewModel;
        }

        [Route("UpdateEmployee")]
        [HttpPut]
        public async Task UpdateEmployee(EmployeeViewModel employeeViewModel)
        {

            await _employeeRepository.Update(employeeViewModel);
        }

        [Route("DeleteEmployee")]
        [HttpDelete]
        public async Task DeleteEmployee(int id)
        {
            await _employeeRepository.Delete(id);
        }

        [HttpGet]
        [Route("SearchEmployee/{searchString}")]
        public async Task<List<EmployeeViewModel>> Search(string searchString)
        {
            var empList = await _employeeRepository.Search(searchString);
            return empList;
        }
    }
}

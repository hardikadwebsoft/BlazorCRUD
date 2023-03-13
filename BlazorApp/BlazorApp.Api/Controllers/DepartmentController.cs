using BlazorApp.Models.ViewModels;
using BlazorApp.Service.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }


        [HttpGet]
        [Route("GetList")]
        public async Task<List<DepartmentViewModel>> GetList()
        {
            var deptList = await _departmentRepository.GetList();
            return deptList;
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<DepartmentViewModel> GetById(int id)
        {
            var deptList = await _departmentRepository.GetById(id);
            return deptList;
        }

    }
}

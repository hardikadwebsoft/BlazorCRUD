using AutoMapper;
using BlazorApp.Data.DataModels;
using BlazorApp.Models.Models;
using BlazorApp.Models.ViewModels;

namespace BlazorApp.Api.AutoMapper
{   
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Add map for all entities and models.
        /// Rules: 
        ///      1. Must add comment above for entity and model so it can be identified.
        ///      2. Each entity and model section must be separated by space.
        /// </summary>
        public MappingProfile() 
        {
            CreateMap<Department, DepartmentViewModel>();

            CreateMap<Employee, EmployeeFormModel>();
            CreateMap<EmployeeFormModel, Employee>();
            CreateMap<Employee, EmployeeViewModel>()
                .ForMember(dest => dest.DepartmentViewModel, opt => opt.MapFrom(src => src.Department));
            CreateMap<EmployeeViewModel, Employee>()
                .ForMember(dest=>dest.Department ,opt=>opt.MapFrom(src=>src.DepartmentViewModel));

            CreateMap<DepartmentViewModel, Department>();
        }
    }
}

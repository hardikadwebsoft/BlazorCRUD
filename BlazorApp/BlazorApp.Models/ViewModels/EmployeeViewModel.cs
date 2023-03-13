using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorApp.Models.ViewModels
{
    public class EmployeeViewModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [EmailAddress]
        [Required(ErrorMessage ="Email is required")]
        public string Email { get; set; }
        
        public string DateOfBirth { get; set; }
        public int DepartmentId { get; set; }

        public DepartmentViewModel DepartmentViewModel { get; set; } = new();
    }

    

}

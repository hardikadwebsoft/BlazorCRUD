using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Models.Models
{
    public class EmployeeFormModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string DateOfBirth { get; set; }
        public string Department { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Models.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DepartmentViewModel()
        {
        }
        public DepartmentViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorApp.Models.ViewModels
{
    public class EmployeeViewModel
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("dateOfBirth")]
        public string DateOfBirth { get; set; }
        [JsonPropertyName("department")]
        public string Department { get; set; }
    }

    

}

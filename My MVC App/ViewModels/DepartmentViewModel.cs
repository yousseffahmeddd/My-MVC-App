using Demo.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;

namespace My_MVC_App.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Code { get; set; } //nullable reference type
        [Required]
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}

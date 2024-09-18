using Demo.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_MVC_App.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int? Age { get; set; }

        public decimal Salary { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime HireDate { get; set; }

        public DateTime DateOfCreation { get; set; }

        public string ImageName { get; set; }

        public IFormFile Image { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}

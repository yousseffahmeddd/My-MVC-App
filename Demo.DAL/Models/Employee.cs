﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class Employee : BaseEntity
    {
        [Required(ErrorMessage ="Name is Required!")]
        public string Name { get; set; }

        [Range(22,45)]
        public int? Age { get; set; }
        public decimal Salary { get; set; }

        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
         ErrorMessage ="Address must be like 123-Street-City-Country")]
        public string Address { get; set; }

        [Phone]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [DisplayName("Hiring Date")]
        public DateTime HireDate { get; set; }

        public string ImageName { get; set; }

        [DisplayName("Date Of Creation")]
        public DateTime DateOfCreation { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}

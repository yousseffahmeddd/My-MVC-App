using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class Department : BaseEntity
    {

        [Required(ErrorMessage ="Code is Required!")]
        public string Code { get; set; } //nullable reference type

        [Required(ErrorMessage = "Code is Required!")]
        public string Name { get; set; }

        [DisplayName("Date Of Creation")]
        public DateTime DateOfCreation { get; set; }

        [ForeignKey(nameof(Employee))]
        public ICollection<Employee> Employees { get; set; }
    }
}

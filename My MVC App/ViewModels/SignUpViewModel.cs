using System.ComponentModel.DataAnnotations;

namespace My_MVC_App.ViewModels
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage = "First Name is Required")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "Last Name is Required")]
		public string LastName { get; set; }


		[Required(ErrorMessage ="User Name is Required")]
        public string UserName { get; set; }	
        [Required(ErrorMessage = "Email is Required")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password is Required")]
		[MinLength(5,ErrorMessage ="Minimum Password Length is 5")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirmed Password is Required")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Password Doesn`t match")]
		public string ConfirmedPassword { get; set; }
		public bool IsAgree { get; set; }
	}
}

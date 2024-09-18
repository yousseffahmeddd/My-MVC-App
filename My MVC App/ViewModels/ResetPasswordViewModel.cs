using System.ComponentModel.DataAnnotations;

namespace My_MVC_App.ViewModels
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "Password is Required")]
		[MinLength(5, ErrorMessage = "Minimum Password Length is 5")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }

		[Required(ErrorMessage = "Confirmed Password is Required")]
		[DataType(DataType.Password)]
		[Compare(nameof(NewPassword), ErrorMessage = "Password Doesn`t match")]
		public string ConfirmedPassword { get; set; }

	}
}

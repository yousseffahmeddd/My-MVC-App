using AutoMapper;
using Demo.DAL.Models;
using Demo_session_3_MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using My_MVC_App.ViewModels;

namespace My_MVC_App.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
            CreateMap<IdentityRole, RoleViewModel>().ForMember(d => d.RoleName, O => O.MapFrom(S => S.Name)).ReverseMap();

            //CreateMap<Employee, EmployeeViewModel>();
        }
    }
}

using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_MVC_App.Helper;
using My_MVC_App.ViewModels;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_MVC_App.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private IUnitOfWork _unitOfWork; //Null
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)//ask clr to create object from DepartmenRepository
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //Get All
        public async Task<IActionResult> Index(string SearchInput)
        { 
            var employees=Enumerable.Empty<Employee>();

            //1. Add
            //2. Update
            //3. Delete


            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = await _unitOfWork.EmployeeRepository.GetAll();

            }
            else
            {
                employees= await _unitOfWork.EmployeeRepository.GetByName(SearchInput.ToLower());
            }

            var result = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            //view data ==key value pair [dicionart object]
            //.netframework 3.5
            //ViewData["Message"] = "hello from view data";
            //viewbag==> dynamic property[based on dynamic keyword ]
            //.netframework 4



            ViewBag.Message = "hello from view bag";
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Departments"] = _unitOfWork.DepartmentRepository.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {

                model.ImageName = DocumentSettings.UploadFile(model.Image, "images");


                var employee = _mapper.Map<Employee>(model);
                _unitOfWork.EmployeeRepository.Add(employee);

                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    TempData["Message"] = "Employee Added";

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = "Employee NOT Added";
                }
            }
            return View();
        }

        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {

            if (id is null)
            {
                return BadRequest(); //400
            }

            var employee = await _unitOfWork.EmployeeRepository.Get(id.Value);

            var result = _mapper.Map<EmployeeViewModel>(employee);

            //EmployeeViewModel employeeViewModel = (EmployeeViewModel) employee;

            if (employee is null)
            {
                return NotFound();
            }

            return View(ViewName, result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {

            return await Details(id, "Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id, EmployeeViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest(); //400
            }

            if(model.ImageName is not null)
            {
                DocumentSettings.DeleteFile(model.ImageName,"images");
            }
            model.ImageName = DocumentSettings.UploadFile(model.Image, "images");

            var employee=_mapper.Map<Employee>(model);
            if (ModelState.IsValid)
            {
                _unitOfWork.EmployeeRepository.Update(employee);

                var count = await _unitOfWork.CompleteAsync();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {

            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? id, EmployeeViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest(); //400
            }

            var employee = _mapper.Map<Employee>(model);



            if (ModelState.IsValid)
            {
                _unitOfWork.EmployeeRepository.Delete(employee);
                var count = await _unitOfWork.CompleteAsync();

                if (count > 0)
                {
                    DocumentSettings.DeleteFile(model.ImageName, "images");
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);

        }
    }
}

using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Data;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace My_MVC_App.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        public IUnitOfWork _unitOfWork { get; }

        //private IDepartmentRepository _departmentRepository; //Null

        public DepartmentController(IUnitOfWork unitOfWork)//ask clr to create object from DepartmenRepository
        {
            _unitOfWork = unitOfWork;
            //_departmentRepository = departmentRepository;
        }

        //Get All
        public async Task<IActionResult> Index()
        {
            var department = await _unitOfWork.DepartmentRepository.GetAll();
            return View(department);
        }

        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department model)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.DepartmentRepository.Add(model);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        public async Task<IActionResult> Details(int? id, string ViewName = "Details" )
        { 

            if(id is null)
            {
                return BadRequest(); //400
            }

            var department = await _unitOfWork.DepartmentRepository.Get(id.Value);

            if(department is null) 
            {
                return NotFound();
            }

            return View(ViewName,department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int? id, Department model)
        {
            if (id != model.Id)
            {
                return BadRequest(); //400
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.DepartmentRepository.Update(model);
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
        public async Task<IActionResult> Delete([FromRoute] int? id, Department model)
        {
            if(id != model.Id)
            {
                return BadRequest(); //400
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.DepartmentRepository.Delete(model);
                var count = await _unitOfWork.CompleteAsync();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);

        }

    }
}

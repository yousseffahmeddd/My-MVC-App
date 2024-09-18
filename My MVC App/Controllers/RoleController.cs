﻿using AutoMapper;
using Demo_session_3_MVC.ViewModels;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_session_3_MVC.Controllers


{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public RoleController(RoleManager<IdentityRole> roleManager ,IMapper mapper)
        {
            _mapper = mapper;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var Roles = await _roleManager.Roles.ToListAsync();
                var MappedRole = _mapper.Map<IEnumerable<IdentityRole>, IEnumerable<RoleViewModel>>(Roles);
                return View(MappedRole);


            }
            else
            {
                var Roles = await _roleManager.FindByNameAsync(SearchValue);
                var MappedRole = _mapper.Map<IdentityRole, RoleViewModel>(Roles);

                return View(new List<RoleViewModel>() { MappedRole });

            }
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Create(RoleViewModel model)
        {
            if(ModelState.IsValid)
            {
                var MappedRole =_mapper.Map<RoleViewModel ,IdentityRole>(model);
                await _roleManager.CreateAsync(MappedRole);
                return RedirectToAction("Index");

            }
            return View(model); 

        }

        public async Task<IActionResult> Details(string Id, string ViewName = "Details")
        {
            if (Id is null)

                return BadRequest();
            var Role = await _roleManager.FindByIdAsync(Id);

            if (Role is null)
                return NotFound();

            var MappedRole = _mapper.Map<IdentityRole, RoleViewModel>(Role);


            return View(ViewName, MappedRole);

        }
        public async Task<IActionResult> Edit(string Id)
        {
            return await Details(Id, "Edit");
        }


        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel model, [FromRoute] string id)

        {
            if (id != model.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try 
                {
                    var Role = await _roleManager.FindByIdAsync(id);
                    Role.Name = model.RoleName;
               

                    await _roleManager.UpdateAsync(Role);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                }

            }
            return View(model);
        }
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");

        }
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                await _roleManager.DeleteAsync(role);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error", "Home");

            }
        }

    }
}


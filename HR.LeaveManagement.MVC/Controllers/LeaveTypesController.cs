using System;
using System.Threading.Tasks;
using HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.MVC.Controllers
{
    public class LeaveTypesController : Controller
    {
        private readonly ILeaveTypeService _leaveTypeRepository;
        private readonly ILeaveAllocationService _leaveAllocationRepository;

        public LeaveTypesController(ILeaveTypeService leaveTypeRepository, ILeaveAllocationService leaveAllocationRepository)
        {
             _leaveTypeRepository = leaveTypeRepository;
             _leaveAllocationRepository = leaveAllocationRepository;
        }
        // GET: LeaveTypesController
        public async Task<ActionResult> Index()
        {
            var model =await _leaveTypeRepository.GetLeaveTypes();
            return View(model); 
        }

        // GET: LeaveTypesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = await _leaveTypeRepository.GetLeaveTypeDetails(id);
            return View(model);
        }

        // GET: LeaveTypesController/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: LeaveTypesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateLeaveTypeVm model)
        {
            try
            {
                var response =await _leaveTypeRepository.CreateLeaveType(model);
                if (response.Success)
                { 
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("",response.ValidationErrors);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
             
            }
            return View(model);
        }

        // GET: LeaveTypesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _leaveTypeRepository.GetLeaveTypeDetails(id);
            return View(model);
        }

        // POST: LeaveTypesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, LeaveTypeVm model)
        {
            try
            {
                var response = await _leaveTypeRepository.UpdateLeaveType(id, model);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index)); 
                }
                ModelState.AddModelError("", response.ValidationErrors);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                
            }
            return View(model);
        }

        // GET: LeaveTypesController/Delete/5
        //[HttpGet]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    var model = await _leaveTypeRepository.GetLeaveTypeDetails(id);
        //    return View(model);
        //}

        // POST: LeaveTypesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var response = await _leaveTypeRepository.DeleteLeaveType(id);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", response.ValidationErrors);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message); 
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Allocate(int id)
        {
            try
            {
                var response =await _leaveAllocationRepository.CreateLeaveAllocations(id);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("",ex.Message);
            }

            return BadRequest();
        }

    }
}

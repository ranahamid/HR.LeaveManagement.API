using System;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HR.LeaveManagement.MVC.Controllers
{
    public class LeaveRequestsController : Controller
    {
        private readonly ILeaveRequestService _leaveRequestRepository; 
        private readonly ILeaveTypeService _leaveTypeRepository;
        private readonly IMapper _mapper;
        public LeaveRequestsController(ILeaveTypeService leaveTypeRepository, ILeaveRequestService leaveRequestRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
        }
        // GET: LeaveRequestsController
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var model =await _leaveRequestRepository.GetAdminLeaveRequestList();
            return View(model);
        }

        // GET: LeaveRequestsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = await _leaveRequestRepository.GetLeaveRequest(id);
            return View(model);
        }

        // GET: LeaveRequestsController/Create
        public async Task<ActionResult> Create()
        {
            var leaveTypes =await  _leaveTypeRepository.GetLeaveTypes();
            var leaveTypeItems = new SelectList(leaveTypes, "Id", "Name");
            var model = new CreateLeaveRequestVm
            {
                LeaveTypes = leaveTypeItems,
            };
            return View(model);
        }

        // POST: LeaveRequestsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Create(CreateLeaveRequestVm model)
        {
            if (ModelState.IsValid)
            {
                var response =await _leaveRequestRepository.CreateLeaveRequest(model);
                if (response .Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", response.ValidationErrors);
            }
            var leaveTypes = await _leaveTypeRepository.GetLeaveTypes();
            var leaveTypeItems = new SelectList(leaveTypes, "Id", "Name");
            model.LeaveTypes = leaveTypeItems;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> ApproveRequest(int id, bool approved)
        {
            try
            {
                 await _leaveRequestRepository.ApproveLeaveRequest(id, approved);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
           
        }
    }
}

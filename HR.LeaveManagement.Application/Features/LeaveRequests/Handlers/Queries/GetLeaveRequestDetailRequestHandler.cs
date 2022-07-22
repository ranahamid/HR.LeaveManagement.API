using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Queries
{
    public class GetLeaveRequestDetailRequestHandler : IRequestHandler<GetLeaveRequestDetailRequest, LeaveRequestDto>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public GetLeaveRequestDetailRequestHandler(
            ILeaveRequestRepository leaveRequestRepository, 
            IUserService userService,
            IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _userService = userService;
            _mapper = mapper;
        }


        async Task<LeaveRequestDto> IRequestHandler<GetLeaveRequestDetailRequest, LeaveRequestDto>.Handle(
            GetLeaveRequestDetailRequest request, CancellationToken cancellationToken)
        {
            var leaveDetails = await _leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);
            var requestData=  _mapper.Map<LeaveRequestDto>(leaveDetails);
            requestData.Employee = await _userService.GetEmployee(requestData.RequestingEmployeeId);
            return requestData;
        }
    }
}
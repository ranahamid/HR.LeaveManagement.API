using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands;
using HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Queries;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries;
using HR.LeaveManagement.Application.Profiles;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Xunit;
using Shouldly;
namespace HR.LeaveManagement.Application.UnitTests.LeaveTypes.Commands
{
    public class CreateLeaveTypeListCommandHanderTests
    {
        // private readonly Mock<ILeaveTypeRepository> _mockRepo;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly IMapper _mapper;
        private readonly CreateLeaveTypeDto _leaveTypeDto;
        private readonly CreateLeaveTypeCommandHandler _handler;
        public CreateLeaveTypeListCommandHanderTests()
        {
            //_mockRepo = MockLeaveTypeRepository.GetLeaveTypeRepository();
            _mockUow = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(x =>
            {
                x.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            _leaveTypeDto = new CreateLeaveTypeDto
            { 
                DefaultDays = 10,
                Name = "Vaccation"
            };
            _handler = new CreateLeaveTypeCommandHandler(_mockUow.Object, _mapper);
        }

        [Fact]
        public async Task ValidLeaveTypeAdded()
        { 
            var result = await _handler.Handle(new CreateLeaveTypeCommand()
            {
                CreateLeaveTypeDto = _leaveTypeDto
            }, CancellationToken.None);

            // var leaveTypes = await _mockRepo.Object.GetAll();
            var leaveTypes = await _mockUow.Object.LeaveTypeRepository.GetAll();
            result.ShouldBeOfType<BaseCommandResponse>();
            leaveTypes.Count.ShouldBe(4);
        }


        [Fact]
        public async Task InValidLeaveTypeAdded()
        {
            _leaveTypeDto.DefaultDays = -1;
            var result = await _handler.Handle(new CreateLeaveTypeCommand()
            {
                CreateLeaveTypeDto = _leaveTypeDto
            }, CancellationToken.None);

            //var leaveTypes = await _mockRepo.Object.GetAll();
            var leaveTypes = await _mockUow.Object.LeaveTypeRepository.GetAll();
            leaveTypes.Count.ShouldBe(3);
          result.ShouldBeOfType<BaseCommandResponse>();
        }
    }
}
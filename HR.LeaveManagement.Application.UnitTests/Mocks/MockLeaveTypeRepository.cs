using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Moq;

namespace HR.LeaveManagement.Application.UnitTests.Mocks
{
    public static  class MockLeaveTypeRepository
    {
        public static Mock<ILeaveTypeRepository> GetLeaveTypeRepository()
        {
            var leaveTypes = new List<LeaveType>
            {
                new()
                {
                    Id = 1,
                    Name = "TEST Vaccation",
                    DefaultDays = 10,
                    //CreatedBy = "",
                    //DateCreated =DateTime.UtcNow ,
                    //LastModifiedBy = "",
                    //LastModifiedDate = DateTime.UtcNow,
                },
                new()
                {
                    Id = 2,
                    Name = "TESTSick",
                    DefaultDays = 12,
                    //CreatedBy = "",
                    //DateCreated = DateTime.UtcNow,
                    //LastModifiedBy = "",
                    //LastModifiedDate = DateTime.UtcNow,
                },
                new()
                {
                    Id = 3,
                    Name = "ano",
                    DefaultDays = 12,
                    //CreatedBy = "",
                    //DateCreated = DateTime.UtcNow,
                    //LastModifiedBy = "",
                    //LastModifiedDate = DateTime.UtcNow,
                }
            };

            var mockRepo = new Mock<ILeaveTypeRepository>();
            mockRepo.Setup(x => x.GetAll()).ReturnsAsync(leaveTypes);
            mockRepo.Setup(x => x.Add(It.IsAny<LeaveType>())).ReturnsAsync((LeaveType leaveType) =>
            {
                leaveTypes.Add(leaveType);
                return leaveType;
            });
            return mockRepo ;
        }
    }
}

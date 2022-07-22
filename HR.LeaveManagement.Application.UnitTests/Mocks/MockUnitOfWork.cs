using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Contracts.Persistence;
using Moq;

namespace HR.LeaveManagement.Application.UnitTests.Mocks
{
    public static  class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetUnitOfWork()
        {
            var mockUow= new Mock<IUnitOfWork>();
            var mockRepo = MockLeaveTypeRepository.GetLeaveTypeRepository();

            mockUow.Setup(x => x.LeaveTypeRepository).Returns(mockRepo.Object);
            return mockUow;
        }
    }
}

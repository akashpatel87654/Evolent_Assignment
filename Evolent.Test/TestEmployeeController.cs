using Evolent_Excercise.Controllers;
using Evolent_Excercise.DataModel;
using Evolent_Excercise.DTO;
using Evolent_Excercise.Repository;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using Moq;
using System.Diagnostics;

namespace Evolent.Test
{
    public class TestEmployeeController
    {
        private readonly Mock<IEmployeeRepository> _employeeRepository;

        //private readonly IEmployeeRepository _employeeRepository;
        public TestEmployeeController()
        {
            _employeeRepository = new Mock<IEmployeeRepository>();
        }

        [Fact]
        public async void GetAllEmployee_ShouldReturnAllEmployee()
        {
            // Arrange
            var repositoryMock = new Mock<IEmployeeRepository>();
            repositoryMock.Setup(x => x.GetEmployees()).ReturnsAsync(new List<Employee> { new Employee { FirstName = "John" }, new Employee { FirstName = "Doe" } });

            var controller = new EmployeeController(repositoryMock.Object);

            // Act
            var result = await controller.GetEmployee();

            // Assert
            Assert.Equal(2, result.ToList().Count);
        }

        [Fact]
        public async void SearchEmployee_ShoulReturnMatchedFristorLastName()
        {
            // Arrange
            var employees = new List<Employee> { new Employee {
                Id = new Guid("6f72c1e8-4574-479f-9143-218f86620cc5"),
                FirstName = "John",
                LastName = "Doe",
                Email = "admin@admin.com",
                Age = 34 } };

            var employeesToReturn = employees.AsQueryable();
            var repositoryMock = new Mock<IEmployeeRepository>();
            repositoryMock.Setup(x => x.SearchEmployee("John Doe")).Returns(employeesToReturn);

            var controller = new EmployeeController(repositoryMock.Object);

            // Act
            var result = await controller.SearchEmployee("John Doe");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John", result.FirstOrDefault().FirstName);
        }

        [Fact]
        public async void CreateEmployee_ShouldCreateEmployee()
        {
            // Arrange
            var repositoryMock = new Mock<IEmployeeRepository>();
            repositoryMock.Setup(x => x.AddEmployee(new Employee { Id = new Guid("6f72c1e8-4574-479f-9143-218f86620cc5"), FirstName = "John", LastName = "Doe", Email = "admin@admin.com", Age = 34 })).ReturnsAsync(new Employee
            {
                Id = new Guid("6f72c1e8-4574-479f-9143-218f86620cc5"),
                FirstName = "John",
                LastName = "Doe",
                Email = "admin@admin.com",
                Age = 34
            });

            // Act
            var controller = new EmployeeController(repositoryMock.Object);
            var result = await controller.PostEmployee(new EmployeeDTO { FirstName = "John", LastName = "Doe", Email = "admin@admin.com", Age = 34 });

            // Assert
            Assert.NotNull(result);
        }

    }
}
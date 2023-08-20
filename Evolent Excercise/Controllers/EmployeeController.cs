using Evolent_Excercise.DataModel;
using Evolent_Excercise.DTO;
using Evolent_Excercise.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Evolent_Excercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Get list of all employee.
        /// </summary>
        /// <returns>Enumerable list of Employee model.</returns>
        [HttpGet]
        [Route("api/Employee/GetAll")]
        public async Task<IEnumerable<Employee>> GetEmployee()
        {
            return await _employeeRepository.GetEmployees();
        }

        /// <summary>
        /// Search employee by either firstName or lastname.
        /// </summary>
        /// <param name="searchKeyWord"></param>
        /// <returns>Enumerable list of Employee model.</returns>
        [HttpGet]
        [Route("api/Employee/Search")]
        public async Task<IEnumerable<Employee>> SearchEmployee(string searchKeyWord)
        {
            var employee = _employeeRepository.SearchEmployee(searchKeyWord);
            return employee.ToList();
        }


        /// <summary>
        /// Create new Employee record.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Newly created employee object.</returns>
        [HttpPost]
        [Route("api/Employee/Create")]
        public async Task<IActionResult> PostEmployee(EmployeeDTO employee)
        {
            try
            {
                //Check if employee with same details is exist.
                var exists = await _employeeRepository.IsEmpExist(employee.FirstName, employee.LastName, employee.Email);
                if (exists)
                {
                    return BadRequest("Employee with the same details is already exist.");
                }

                //Create Employee model.
                var emp = new Employee()
                {
                    Id = new Guid(),
                    Address = employee.Address,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Age = employee.Age,
                    Email = employee.Email,
                };

                return Ok(await _employeeRepository.AddEmployee(emp));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update employee details.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Updated employee model.</returns>
        [HttpPut]
        [Route("api/Employee/Update")]
        public async Task<IActionResult> UpdateEmployee(Employee employee)
        {
            try
            {
                // Check if the employee exists.
                var existingEmployee = await _employeeRepository.GetEmployee(employee.Id);
                if (existingEmployee == null)
                {
                    return NotFound();
                }

                // Check if the first name, last name, and email are unique.
                var employeeExists = await _employeeRepository.IsEmpExist(employee.FirstName, employee.LastName, employee.Email);
                if (employeeExists)
                {
                    return BadRequest("The first name, last name, and email must be unique.");
                }

                //update employee.
                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.Email = employee.Email;
                existingEmployee.Address = employee.Address;
                existingEmployee.Age = employee.Age;
                return Ok(await _employeeRepository.UpdateEmployee(existingEmployee));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete Employee record.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/Employee/Delete")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            try
            {
                // Check if the employee exists.
                var existingEmployee = await _employeeRepository.GetEmployee(id);
                if (existingEmployee == null)
                {
                    return NotFound();
                }

                // Delete the employee.
                await _employeeRepository.DeleteEmployee(id);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using Evolent_Excercise.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Evolent_Excercise.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _context;

        public EmployeeRepository(DataContext context)
        {
            _context = context;

            //load demo data if database is empty.
            if (_context.Employees.Count() == 0)
            {
                _context.Employees.Add(new Employee()
                {
                    Id = new Guid(),
                    FirstName = "Akash",
                    LastName = "Patel",
                    Email = "akash.patel@gmail.com",
                    Address = "Test",
                    Age = 29
                });

                _context.Employees.Add(new Employee()
                {
                    Id = new Guid(),
                    FirstName = "Demo",
                    LastName = "Patel",
                    Email = "demo.patel@gmail.com",
                    Address = "India",
                    Age = 22
                });

                _context.SaveChanges();
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployee(Guid id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<bool> IsEmpExist(string firstName, string lastName, string email)
        {
            return await _context.Employees.AnyAsync(e => e.FirstName == firstName && e.LastName == lastName && e.Email == email);
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task DeleteEmployee(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Employee> SearchEmployee(string searchWord)
        {
            searchWord = searchWord.ToUpper();
            return _context.Employees.Where(e => e.FirstName.ToUpper().Contains(searchWord) || e.LastName.ToUpper().Contains(searchWord));
        }
    }
}

using Evolent_Excercise.DataModel;

namespace Evolent_Excercise.Repository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(Guid id);
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee employee);
        Task DeleteEmployee(Guid id);
        Task<bool> IsEmpExist(string firstName, string lastName, string email);
        IQueryable<Employee> SearchEmployee(string searchWord);
    }
}

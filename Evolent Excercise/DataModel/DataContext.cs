using Microsoft.EntityFrameworkCore;

namespace Evolent_Excercise.DataModel
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}

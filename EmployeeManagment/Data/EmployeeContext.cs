using EmployeeManagment.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagment.Data
{
    public class EmployeeContext :DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {

        }
        public DbSet<Employee> tbl_Employee { get; set; }
        public DbSet<Attendence> tbl_Attendence { get; set; }
    }
}

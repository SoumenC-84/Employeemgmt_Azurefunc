using Microsoft.EntityFrameworkCore;

public class EmployeeRepository : IEmployeeService
{
    private readonly EmployeeDbContext _dbContext;

    public EmployeeRepository(EmployeeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        return await _dbContext.Employees.ToListAsync();
    }

    public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
    {
        return await _dbContext.Employees.FindAsync(employeeId);
    }

    public async Task<Employee> AddEmployeeAsync(Employee employee)
    {
        _dbContext.Employees.Add(employee);
        await _dbContext.SaveChangesAsync();
        return employee;
    }

    public async Task<Employee> UpdateEmployeeAsync(Employee employee)
    {
        _dbContext.Entry(employee).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return employee;
    }

    public async Task<bool> DeleteEmployeeAsync(int employeeId)
    {
        var employee = await _dbContext.Employees.FindAsync(employeeId);
        if (employee == null)
            return false;

        _dbContext.Employees.Remove(employee);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}
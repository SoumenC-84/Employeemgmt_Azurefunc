using Microsoft.EntityFrameworkCore;

public class EmployeeDbContext:DbContext
{
    public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
    {
        
    }
    public DbSet<Employee> Employees { get; set; }  

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Employee>().HasKey(e => e.EmployeeId);
        modelBuilder.Entity<Employee>().Property(e => e.FirstName).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Employee>().Property(e => e.LastName).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Employee>().Property(e => e.Email).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Employee>().Property(e => e.Department).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Employee>().Property(e => e.Salary).HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Employee>().Property(e => e.CreatedDate)
            .HasDefaultValueSql("SYSUTCDATETIME()");
    }
    
}
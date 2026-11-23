
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((context, config) =>
    {
        var connectionString = Environment.GetEnvironmentVariable("Sqlconnectionstring");    
        config.AddDbContext<EmployeeDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        }); 
        config.AddScoped<IEmployeeService, EmployeeRepository>();
    })
    .Build();
    
host.Run();

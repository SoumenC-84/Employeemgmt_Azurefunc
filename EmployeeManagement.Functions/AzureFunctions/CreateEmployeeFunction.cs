using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

public class CreateEmployeeFunction
{
    private readonly IEmployeeService _employeeService;
    private readonly ILogger<CreateEmployeeFunction> _logger;

    public CreateEmployeeFunction(IEmployeeService employeeService, ILogger<CreateEmployeeFunction> logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }

    [Function("CreateEmployee")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "employees")]
         HttpRequest req)
    {
        _logger.LogInformation("Processing request to create a new employee.");

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var employee = JsonConvert.DeserializeObject<Employee>(requestBody);

        if (employee == null)
        {
            return new BadRequestObjectResult("Invalid employee data.");
        }

        var createdEmployee = await _employeeService.AddEmployeeAsync(employee);
        return new OkObjectResult(createdEmployee);
    }
}
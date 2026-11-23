using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

public class ReadEmployeeFunction
{
    private readonly IEmployeeService _employeeService;
    private readonly ILogger<ReadEmployeeFunction> _logger;

    public ReadEmployeeFunction(IEmployeeService employeeService, ILogger<ReadEmployeeFunction> logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }

    [Function("ReadEmployee")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "employees/{id}")]
         HttpRequest req, string id)
    {
        _logger.LogInformation($"Processing request to read employee with ID: {id}.");

        if (!int.TryParse(id, out int employeeId))
        {
            return new BadRequestObjectResult("Invalid employee ID.");
        }

        var employee = await _employeeService.GetEmployeeByIdAsync(employeeId);
        if (employee == null)
        {
            return new NotFoundResult();
        }

        return new OkObjectResult(employee);
    }
}
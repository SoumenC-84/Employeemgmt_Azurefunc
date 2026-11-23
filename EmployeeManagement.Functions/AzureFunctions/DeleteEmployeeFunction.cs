using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

public class DeleteEmployeeFunction
{
    private readonly IEmployeeService _employeeService;
    private readonly ILogger<DeleteEmployeeFunction> _logger;

    public DeleteEmployeeFunction(IEmployeeService employeeService, ILogger<DeleteEmployeeFunction> logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }

    [Function("DeleteEmployee")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "employees/{id}")]
         HttpRequest req, string id)
    {
        _logger.LogInformation($"Processing request to delete employee with ID: {id}.");

        if (!int.TryParse(id, out int employeeId))
        {
            return new BadRequestObjectResult("Invalid employee ID.");
        }

        var result = await _employeeService.DeleteEmployeeAsync(employeeId);
        if (!result)
        {
            return new NotFoundResult();
        }

        return new OkResult();
    }
}
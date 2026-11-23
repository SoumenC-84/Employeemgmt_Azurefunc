using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

public class UpdateEmployeeFunction
{
    private readonly IEmployeeService _employeeService;
    private readonly ILogger<UpdateEmployeeFunction> _logger;

    public UpdateEmployeeFunction(IEmployeeService employeeService, ILogger<UpdateEmployeeFunction> logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }

    [Function("UpdateEmployee")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "put", Route = "employees/{id}")]
         HttpRequest req, string id)
    {
        _logger.LogInformation($"Processing request to update employee with ID: {id}.");

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var updatedEmployee = JsonConvert.DeserializeObject<Employee>(requestBody);

        if (updatedEmployee == null)
        {
            return new BadRequestObjectResult("Invalid employee data.");
        }

        var result = await _employeeService.UpdateEmployeeAsync(updatedEmployee);
        if (result == null)
        {
            return new NotFoundResult();
        }

        return new OkObjectResult(result);
    }
}
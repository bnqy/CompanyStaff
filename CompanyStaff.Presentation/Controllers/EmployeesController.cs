using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStaff.Presentation.Controllers;

[ApiController]
[Route("api/companies/{companyId}/employees")]
public class EmployeesController : ControllerBase
{
    private readonly IServiceManager serviceManager;

	public EmployeesController(IServiceManager serviceManager)
	{
		this.serviceManager = serviceManager;
	}

	[HttpGet]
	public IActionResult GetEmployeesWithCompanyId(Guid companyId)
	{
		var employees = serviceManager.EmployeeService.GetEmployees(companyId, false);

		return Ok(employees);
	}
}

using Microsoft.AspNetCore.Mvc;

namespace xUnit.API.Controllers
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
    }
}

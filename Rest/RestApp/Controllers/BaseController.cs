using Microsoft.AspNetCore.Mvc;

namespace RestApp.API.Controllers
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
    }
}

using Microsoft.AspNetCore.Mvc;
using PaymentGateway.API.Middleware.Security;

namespace PaymentGateway.API.Controllers
{
    [ApiKeySecurity]
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
    }
}

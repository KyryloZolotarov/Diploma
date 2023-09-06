using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Order.Hosts.Controllers
{
    [ApiController]
    //[Authorize(Policy = AuthPolicy.AllowClientPolicy)]
    //[Scope("order.orderuser")]
    //[Route(ComponentDefaults.DefaultRoute)]
    public class UserController : ControllerBase
    {
    }
}

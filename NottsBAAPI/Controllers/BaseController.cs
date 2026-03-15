using Microsoft.AspNetCore.Mvc;

namespace NottsBAAPI.Controllers;

[ApiController]
[Route("/[controller]")]
public class BaseController : ControllerBase
{
    public BaseController()
    {

    }
}

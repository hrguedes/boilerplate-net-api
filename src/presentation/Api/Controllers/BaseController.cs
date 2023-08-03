using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class BaseController : ControllerBase
{
    /// <summary>
    /// Get User Id
    /// </summary>
    /// <returns></returns>
    protected string GetIdUser() => User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
}
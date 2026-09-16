using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace dotnetapp.Controllers;

[ApiController, Route("api/cakes"), Authorize]
public class CakeController(ICakeService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cake>>> All() => Ok(await service.GetAllCakes());
    [HttpGet("{cakeId:int}")]
    public async Task<IActionResult> One(int cakeId) { var x = await service.GetCakeById(cakeId); return x == null ? NotFound(new { message = "Cannot find any cake" }) : Ok(x); }
    [HttpPost, Authorize(Roles = UserRoles.Baker)]
    public async Task<IActionResult> Add(Cake cake) => await service.AddCake(cake) ? Ok(new { message = "Cake added successfully" }) : BadRequest(new { message = "Cake already exists" });
    [HttpPut("{cakeId:int}"), Authorize(Roles = UserRoles.Baker)]
    public async Task<IActionResult> Update(int cakeId, Cake cake) => await service.UpdateCake(cakeId, cake) ? Ok(new { message = "Cake updated successfully" }) : NotFound(new { message = "Cannot find any cake" });
    [HttpDelete("{cakeId:int}"), Authorize(Roles = UserRoles.Baker)]
    public async Task<IActionResult> Delete(int cakeId) => await service.DeleteCake(cakeId) ? Ok(new { message = "Cake deleted successfully" }) : NotFound(new { message = "Cannot find any cake" });
}

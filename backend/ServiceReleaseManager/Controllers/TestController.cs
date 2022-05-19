using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceReleaseManager.DataAccess;
using ServiceReleaseManager.Domain.Models.User;

namespace ServiceReleaseManager.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly UserDbContext _userDbContext;

    public TestController(UserDbContext userDbContext)
    {
        _userDbContext = userDbContext;
    }

    [HttpGet("Test")]
    public async Task<IReadOnlyCollection<User>> Test()
    {
        return await _userDbContext.Users.ToListAsync();
    }

    [HttpGet("Test1")]
    public async Task<ActionResult> Test1()
    {
        var user = new User();
        user.Admin = false;
        user.Email = "test@localhost";
        _userDbContext.Users.Add(user);
        await _userDbContext.SaveChangesAsync();
        
        return Accepted(user);
    }
}
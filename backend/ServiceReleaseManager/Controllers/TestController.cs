using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceReleaseManager.DataAccess;
using ServiceReleaseManager.Domain.Models.User;

namespace ServiceReleaseManager.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly ServiceReleaseManagerDbContext _dbContext;

    public TestController(ServiceReleaseManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("Test")]
    public async Task<IReadOnlyCollection<User>> Test()
    {
        return await _dbContext.Users.ToListAsync();
    }

    [HttpGet("Test1")]
    public async Task<ActionResult> Test1()
    {
        var user = new User();
        user.Admin = false;
        user.Email = "test@localhost";
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
        
        return Accepted(user);
    }
}
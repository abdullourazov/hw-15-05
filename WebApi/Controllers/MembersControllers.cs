using Domain.Entities;
using Infrastructure.Interface;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;


[ApiController]
[Route("api/[Controller]")]

public class MembersControllers : ControllerBase
{
    private IMembersServices membersServices = new MembersServices();

    [HttpPost]
    public async Task<int> AddMembersAsync(Members members)
    {
        var result = await membersServices.AddMembersAsync(members);
        return result;
    }

    [HttpGet]
    public async Task<List<Members>> WatchesMembersAsync()
    {
        var result = await membersServices.WatchesMembersAsync();
        return result;
    }

    [HttpGet("{id}")]
    public async Task<Members?> GetMembersAsyncById(int id)
    {
        var result = await membersServices.GetMembersByIdAsync(id);
        return result;
    }

    [HttpPut]
    public async Task<bool> UpdateMembersAsync(Members members)
    {
        var result = await membersServices.UpdateMembersAsync(members);
        return result;
    }

    [HttpDelete]
    public async Task<bool> DeleteMembersAsync(Members members)
    {
        var result = await membersServices.DeleteMembersAsync(members);
        return result;
    }
}

using Domain.Entities;
using Infrastructure.Interface;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;


[ApiController]
[Route("api/[Controller]")]

public class MembersControllers(IMembersServices membersServices)
{

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
    public async Task<Members?> GetMembersByIdAsync(int id)
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

    [HttpGet("active/{id}")]
    public async Task<Members?> ActiveMembersAsync(int id)
    {
        return await membersServices.ActiveMembersAsync(id);
    }

    [HttpGet("count ")]
    public async Task<int> CountMembersOneBorrowingsAsync(Members members)
    {
        return await membersServices.CountMembersOneBorrowingsAsync(members);
    }
}

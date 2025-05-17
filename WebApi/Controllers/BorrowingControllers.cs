using Domain.Entities;
using Infrastructure.Interface;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class BorrowingControllers : ControllerBase
{
    private IBorrowingsServices borrowingsServices = new BorrowingsServices();

    [HttpGet]
    public async Task<List<Borrowings>> GetAllBorrowingsAsync()
    {
        return await borrowingsServices.GetAllBorrowingsAsync();
    }

    [HttpGet("memberId")]
    public async Task<Borrowings?> GetBorrowingsByMemberId(int memberId)
    {
        return await borrowingsServices.GetBorrowingsByMemberId(memberId);
    }

    [HttpPost]
    public async Task<string> CreateBorrowingAsync(Borrowings borrowings)
    {
        return await borrowingsServices.CreateBorrowingAsync(borrowings);
    }

    [HttpGet("BorrowingId")]
    public async Task<string> ReturnBookAsync(int BorrowingId)
    {
        return await borrowingsServices.ReturnBookAsync(BorrowingId);
    }


}

using Domain.ApiResponsice;
using Domain.Entities;
using Infrastructure.Interface;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;


[ApiController]
[Route("api/[Controller]")]

public class BorrowingControllers(IBorrowingsServices borrowingsServices)
{
    [HttpGet]
    public async Task<Response<List<Borrowings>>> GetAllBorrowingsAsync()
    {
        return await borrowingsServices.GetAllBorrowingsAsync();
    }

    [HttpGet("memberId")]
    public async Task<Response<Borrowings?>> GetBorrowingsByMemberIdAsync(int memberId)
    {
        return await borrowingsServices.GetBorrowingsByMemberIdAsync(memberId);
    }

    [HttpPost]
    public async Task<Response<string>> CreateBorrowingAsync(Borrowings borrowings)
    {
        return await borrowingsServices.CreateBorrowingAsync(borrowings);
    }

    [HttpGet("BorrowingId")]
    public async Task<Response<string>> ReturnBookAsync(int BorrowingId)
    {
        return await borrowingsServices.ReturnBookAsync(BorrowingId);
    }

    [HttpGet("count/{id}")]
    public async Task<Response<int>> GetAllCountBorrowingsAsync(int id)
    {
        return await borrowingsServices.GetAllCountBorrowingsAsync(id);
    }

    [HttpGet("avg/{id}")]
    public async Task<Response<decimal>> AvgShtrafSrokAsync(int id)
    {
        return await borrowingsServices.AvgShtrafSrokAsync(id);
    }





}

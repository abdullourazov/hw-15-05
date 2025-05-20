using System.Net;
using Dapper;
using Domain.ApiResponsice;
using Domain.Entities;
using Infrastructure.Date;
using Infrastructure.Interface;

namespace Infrastructure.Services;

public class MembersServices(DataContext context) : IMembersServices
{
    public async Task<Response<int>> AddMembersAsync(Members members)
    {
        using var connection = await context.GetConnection();
        var cmd = @"insert into members (Fullname, Phone, Email,  MembershipDate)
                    values (@Fullname, @Phone, @email, @MembershipDate)";
        var result = await connection.ExecuteAsync(cmd, members);
        return result == 0 ? new Response<int>("Member not found", HttpStatusCode.NotFound)
        : new Response<int>(result, "Member succesfully retrieved");
    }

    public async Task<Response<List<Members>>> WatchesMembersAsync()
    {
        using var connection = await context.GetConnection();
        var cmd = @"select * from members";
        var result = await connection.QueryAsync<Members>(cmd);
        return result == null ? new Response<List<Members>>("Memeber not foind", HttpStatusCode.NotFound)
        : new Response<List<Members>>(result.ToList(), "Borrowing succesfully retrieved");
    }

    public async Task<Response<Members?>> GetMembersByIdAsync(int id)
    {
        using var connection = await context.GetConnection();
        var cmd = @"select * from members
                    where  Memberid = @Memberid";
        var result = await connection.QuerySingleOrDefaultAsync<Members>(cmd, new { Memberid = id });
        return result == null ? new Response<Members?>("Member not found", HttpStatusCode.NotFound)
        : new Response<Members?>(result, "Borroowing succesfully retrieved");
    }

    public async Task<Response<bool>> UpdateMembersAsync(Members members)
    {
        using var connection = await context.GetConnection();
        var cmd = @"update members
                    set Fullname = @Fullname,
                        Phone = @Phone,
                        Email = @email,
                        MembershipDate = @MembershipDate
                        where Memberid = @Memberid";
        var result = await connection.ExecuteAsync(cmd, members);
        return result == 0 ? new Response<bool>("Member not found", HttpStatusCode.NotFound)
        : new Response<bool>(true, "Borrowing succesfully retrieved");
    }

    public async Task<Response<bool>> DeleteMembersAsync(Members members)
    {
        using var connection = await context.GetConnection();
        var cmd = @"delete from  members
                    where Memberid = @Memberid
                    and not exists (
                    select 1 from borrowings
                    where borrowings.Memberid = books.Memberid
                    and MembershipDate IS NULL)";
        var result = await connection.ExecuteAsync(cmd, members);
        return result == 0 ? new Response<bool>("Member not found", HttpStatusCode.NotFound)
       : new Response<bool>(true, "Borrowing succesfully retrieved");
    }

    //2

    public async Task<Response<Members?>> ActiveMembersAsync(int id)
    {
        using var connection = await context.GetConnection();
        var cmd = @"select m.* from members m
                    join borrowings b on m.memberid = b.borrowingid
                    group by m.memberid, m.fullname, m.phone, m.email, m.membershipdate
                    order by count(b.borrowingid) desc
                    limit 1";
        var result = await connection.QuerySingleOrDefaultAsync<Members?>(cmd, new { memberId = id });
        return result == null ? new Response<Members?>("Member not found", HttpStatusCode.NotFound)
        : new Response<Members?>(result, "Borroowing succesfully retrieved");
    }

    //8
    public async Task<Response<int>> CountMembersOneBorrowingsAsync(Members members)
    {
        using var connection = await context.GetConnection();
        var cmd = @"select count(distinct m.memberid) from members m
                    join borrowings b on b.memberid = m.memberid";
        var result = await connection.ExecuteScalarAsync<int>(cmd, members);
        return result == 0 ? new Response<int>("Member not found", HttpStatusCode.NotFound)
       : new Response<int>(result, "Member succesfully retrieved");
    }


}

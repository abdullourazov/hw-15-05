using Dapper;
using Domain.Entities;
using Infrastructure.Date;
using Infrastructure.Interface;

namespace Infrastructure.Services;

public class MembersServices : IMembersServices
{
    private readonly DataContext context = new();

    public async Task<int> AddMembersAsync(Members members)
    {
        using var connection = await context.GetConnection();
        var cmd = @"insert into members (Fullname, Phone, Email,  MembershipDate)
                    values (@Fullname, @Phone, @email, @MembershipDate)";
        var result = await connection.ExecuteAsync(cmd, members);
        return result;
    }

    public async Task<List<Members>> WatchesMembersAsync()
    {
        using var connection = await context.GetConnection();
        var cmd = @"select * from members";
        var result = await connection.QueryAsync<Members>(cmd);
        return result.ToList();
    }

    public async Task<Members?> GetMembersByIdAsync(int id)
    {
        using var connection = await context.GetConnection();
        var cmd = @"select * from members
                    where  Memberid = @Memberid";
        var result = await connection.QuerySingleOrDefaultAsync<Members>(cmd, new { Memberid = id });
        return result;
    }

    public async Task<bool> UpdateMembersAsync(Members members)
    {
        using var connection = await context.GetConnection();
        var cmd = @"update members
                    set Fullname = @Fullname,
                        Phone = @Phone,
                        Email = @email,
                        MembershipDate = @MembershipDate
                        where Memberid = @Memberid";
        var result = await connection.ExecuteAsync(cmd, members);
        return result > 0;
    }

    public async Task<bool> DeleteMembersAsync(Members members)
    {
        using var connection = await context.GetConnection();
        var cmd = @"delete from  members
                    where Memberid = @Memberid
                    and not exists (
                    select 1 from borrowings
                    where borrowings.Memberid = books.Memberid
                    and MembershipDate IS NULL)";
        var result = await connection.ExecuteAsync(cmd, members);
        return result > 0;
    }

    //2

    public async Task<Members?> ActiveMembers(int id)
    {
        using var connection = await context.GetConnection();
        var cmd = @"select m.* from members m
                    join borrowings b on m.memberid = b.borrowingid
                    group by m.memberid, m.fullname, m.phone, m.email, m.membershipdate
                    order by count(b.borrowingid) desc
                    limit 1";
        var result = await connection.QuerySingleOrDefaultAsync<Members?>(cmd, new { memberId = id });
        return result;
    }

    
}

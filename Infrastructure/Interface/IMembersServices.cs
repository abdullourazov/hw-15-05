using Domain.Entities;

namespace Infrastructure.Interface;

public interface IMembersServices
{
    Task<int> AddMembersAsync(Members members);
    Task<List<Members>> WatchesMembersAsync();
    Task<Members?> GetMembersByIdAsync(int id);
    Task<bool> UpdateMembersAsync(Members members);
    Task<bool> DeleteMembersAsync(Members members);
    Task<Members?> ActiveMembers(int id);
    
}

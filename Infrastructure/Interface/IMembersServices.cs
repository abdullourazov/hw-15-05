using Domain.ApiResponsice;
using Domain.Entities;

namespace Infrastructure.Interface;

public interface IMembersServices
{
    Task<Response<int>> AddMembersAsync(Members members);
    Task<Response<List<Members>>> WatchesMembersAsync();
    Task<Response<Members?>> GetMembersByIdAsync(int id);
    Task<Response<bool>> UpdateMembersAsync(Members members);
    Task<Response<bool>> DeleteMembersAsync(Members members);
    Task<Response<Members?>> ActiveMembersAsync(int id);
    Task<Response<int>> CountMembersOneBorrowingsAsync(Members members);
}

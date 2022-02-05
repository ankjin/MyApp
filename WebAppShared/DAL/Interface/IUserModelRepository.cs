using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppShared.ModelsApi;

namespace WebAppShared.DAL.Interface
{
    public interface IUserModelRepository : IRepository<User>
    {
        IEnumerable<User> GetAllWithRole();
        ValueTask<IEnumerable<User>> GetAllWithRoleAsync();
        IEnumerable<User> GetAllWithRoleWithSourcePartner();
        ValueTask<IEnumerable<User>> GetAllWithRoleWithSourcePartnerAsync();
        User GetUserWithRoleAndInvestment(Guid id);
        User GetUserWithRole(string email);
        User GetUserWithRoleSourcePartner(string email);
        IEnumerable<User> GetUserWithReferalId(string refid);
        ValueTask<IEnumerable<User>> GetUserWithReferalIdAsync(string refid);
    }
}

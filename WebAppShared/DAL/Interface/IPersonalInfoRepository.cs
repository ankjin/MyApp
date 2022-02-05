using System;
using System.Threading.Tasks;
using WebAppShared.Models;

namespace WebAppShared.DAL.Interface
{
    public interface IPersonalInfoRepository : IRepository<PersonalInfo>
    {
        PersonalInfo GetPersonalInfoByUserId(Guid id);
        ValueTask<PersonalInfo> GetPersonalInfoByUserIdAsync(Guid id);
    }
}

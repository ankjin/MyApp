using System;
using System.Threading.Tasks;
using WebAppShared.Models;

namespace WebAppShared.DAL.Interface
{
    public interface INomineeInfoRepository : IRepository<NomineeInfo>
    {
        NomineeInfo GetNomineeInfoByUserId(Guid id);
        ValueTask<NomineeInfo> GetNomineeInfoByUserIdAsync(Guid id);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppShared.Models;

namespace WebAppShared.DAL.Interface
{
    public interface IBankingInfoRepository : IRepository<BankingInfo>
    {
        IEnumerable<BankingInfo> GetBankingInfoByUserId(Guid id);
        ValueTask<IEnumerable<BankingInfo>> GetBankingInfoByUserIdAsync(Guid id);
    }
}

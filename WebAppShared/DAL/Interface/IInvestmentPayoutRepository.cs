using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppShared.Models;

namespace WebAppShared.DAL.Interface
{
    public interface IInvestmentPayoutRepository : IRepository<InvestmentPayout>
    {
        IEnumerable<InvestmentPayout> GetPayoutsByInvestmentId(Guid id);
        ValueTask<IEnumerable<InvestmentPayout>> GetPayoutsByInvestmentIdAsync(Guid id);
    }
}

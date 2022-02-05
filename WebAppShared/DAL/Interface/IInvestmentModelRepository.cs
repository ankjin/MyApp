using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppShared.Models;

namespace WebAppShared.DAL.Interface
{
    public interface IInvestmentModelRepository : IRepository<InvestmentModel>
    {
        InvestmentModel GetInvestmentByIdWithPayouts(Guid id);
        IEnumerable<InvestmentModel> GetInvestmentByUserId(Guid id);

        ValueTask<IEnumerable<InvestmentModel>> GetInvestmentByUserIdWithPayoutsAsync(Guid id);
        ValueTask<IEnumerable<InvestmentModel>> GetInvestmentByUserIdAsync(Guid id);

        decimal GetTotalInvestmentAmountByUserId(Guid id);

    }
}

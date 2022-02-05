using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppShared.DAL.Interface;
using WebAppShared.Models;

namespace WebAppShared.DAL.Repository
{
    public class InvestmentPayoutRepository : Repository<InvestmentPayout>, IInvestmentPayoutRepository
    {
        public InvestmentPayoutRepository(AppDataContext context) : base(context)
        {

        }

        public IEnumerable<InvestmentPayout> GetPayoutsByInvestmentId(Guid id)
        {
            try
            {
                return dbSet.Where(x => x.InvestmentModelId == id).ToList();
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "{Repo} GetAllWithRole error", typeof(User));

                return new List<InvestmentPayout>();
            }

        }
        public async ValueTask<IEnumerable<InvestmentPayout>> GetPayoutsByInvestmentIdAsync(Guid id)
        {
            try
            {
                return await dbSet.Where(x => x.InvestmentModelId == id).ToListAsync();
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "{Repo} GetAllWithRole error", typeof(User));

                return new List<InvestmentPayout>();
            }
        }
    }
}

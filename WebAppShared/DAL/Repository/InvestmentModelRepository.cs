using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppShared.DAL.Interface;
using WebAppShared.Models;

namespace WebAppShared.DAL.Repository
{
    public class InvestmentModelRepository : Repository<InvestmentModel>, IInvestmentModelRepository
    {
        public InvestmentModelRepository(AppDataContext context) : base(context)
        {

        }

        public InvestmentModel GetInvestmentByIdWithPayouts(Guid id)
        {
            return dbSet.Where(x => x.IsActive && x.Id == id).Include(x => x.InvestmentPayouts).FirstOrDefault();
        }

        public IEnumerable<InvestmentModel> GetInvestmentByUserId(Guid id)
        {
            return dbSet.Where(x => x.IsActive && x.UserModelId == id).Include(x => x.User).ToList();
        }

        public async ValueTask<IEnumerable<InvestmentModel>> GetInvestmentByUserIdWithPayoutsAsync(Guid id)
        {
            return await dbSet.Where(x => x.IsActive && x.UserModelId == id).Include(x => x.User).Include(x => x.InvestmentPayouts).ToListAsync();
        }

        public async ValueTask<IEnumerable<InvestmentModel>> GetInvestmentByUserIdAsync(Guid id)
        {
            return await dbSet.Where(x => x.IsActive && x.UserModelId == id).Include(x => x.User).Include(x => x.InvestmentPayouts).ToListAsync();
        }

        public decimal GetTotalInvestmentAmountByUserId(Guid id)
        {
            return ((decimal)dbSet.Where(x => x.IsActive && x.UserModelId == id).Include(x => x.User).ToList().Sum(x => x.InvestAmount));
        }
    }
}

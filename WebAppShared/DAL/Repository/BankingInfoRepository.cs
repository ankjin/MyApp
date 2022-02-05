using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppShared.DAL.Interface;
using WebAppShared.Models;

namespace WebAppShared.DAL.Repository
{
    public class BankingInfoRepository : Repository<BankingInfo>, IBankingInfoRepository
    {
        public BankingInfoRepository(AppDataContext context) : base(context)
        {

        }

        public IEnumerable<BankingInfo> GetBankingInfoByUserId(Guid id)
        {
            try
            {
                return dbSet.Where(x => x.UserId == id).ToList();
                //return _context.BankingInfos.Where(x => x.UserId == id).ToList();
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "{Repo} GetBankingInfoByUserId error", typeof(BankingInfo));

                return new List<BankingInfo>();
            }
        }
        public async ValueTask<IEnumerable<BankingInfo>> GetBankingInfoByUserIdAsync(Guid id)
        {
            try
            {
                return await dbSet.Where(x => x.UserId == id).ToListAsync();
                //return _context.BankingInfos.Where(x => x.UserId == id).ToListAsync();

            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "{Repo} GetBankingInfoByUserIdAsync error", typeof(BankingInfo));

                return new List<BankingInfo>();
            }
        }

    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebAppShared.DAL.Interface;
using WebAppShared.Models;

namespace WebAppShared.DAL.Repository
{
    public class NomineeInfoRepository : Repository<NomineeInfo>, INomineeInfoRepository
    {
        public NomineeInfoRepository(AppDataContext context) : base(context)
        {

        }

        public NomineeInfo GetNomineeInfoByUserId(Guid id)
        {

            try
            {
                return dbSet.Where(x => x.UserId == id).FirstOrDefault();
                //return _context.NomineeInfos.Where(x => x.UserId == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "{Repo} GetNomineeInfoByUserId error", typeof(NomineeInfo));

                return new NomineeInfo();
            }
        }

        public async ValueTask<NomineeInfo> GetNomineeInfoByUserIdAsync(Guid id)
        {
            try
            {
                return await dbSet.Where(x => x.UserId == id).FirstOrDefaultAsync();
                //return _context.NomineeInfos.Where(x => x.UserId == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "{Repo} GetNomineeInfoByUserIdAsync error", typeof(NomineeInfo));

                return new NomineeInfo();
            }
        }
    }
}

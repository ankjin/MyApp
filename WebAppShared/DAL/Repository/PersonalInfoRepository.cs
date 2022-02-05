using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebAppShared.DAL.Interface;
using WebAppShared.Models;

namespace WebAppShared.DAL.Repository
{
    public class PersonalInfoRepository : Repository<PersonalInfo>, IPersonalInfoRepository
    {
        public PersonalInfoRepository(AppDataContext context) : base(context)
        {

        }

        public PersonalInfo GetPersonalInfoByUserId(Guid id)
        {
            try
            {
                return dbSet.Where(x => x.UserId == id).Include(x => x.User).FirstOrDefault();
                //return _context.PersonalInfos.Where(x => x.UserId == id).Include(x => x.User).FirstOrDefault();
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "{Repo} GetNomineeInfoByUserId error", typeof(PersonalInfo));

                return new PersonalInfo();
            }
        }
        public async ValueTask<PersonalInfo> GetPersonalInfoByUserIdAsync(Guid id)
        {
            try
            {
                return await dbSet.Where(x => x.UserId == id).Include(x => x.User).FirstOrDefaultAsync();
                //return _context.PersonalInfos.Where(x => x.UserId == id).Include(x => x.User).FirstOrDefault();
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "{Repo} GetPersonalInfoByUserIdAsync error", typeof(PersonalInfo));

                return new PersonalInfo();
            }
        }
    }
}

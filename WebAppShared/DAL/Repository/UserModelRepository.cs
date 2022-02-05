using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppShared.DAL.Interface;
using WebAppShared.ModelsApi;

namespace WebAppShared.DAL.Repository
{
    public class UserModelRepository : Repository<User>, IUserModelRepository
    {
        public UserModelRepository(AppDataContext context) : base(context)
        {
        }

        public IEnumerable<User> GetAllWithRole()
        {
            try
            {
                return dbSet.Include(x => x.Role).ToList();
                //return _context.Users.Include(x => x.Role).ToList();
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "{Repo} GetAllWithRole error", typeof(User));

                return new List<User>();
            }
        }
        public async ValueTask<IEnumerable<User>> GetAllWithRoleAsync()
        {
            try
            {
                return await dbSet.Include(x => x.Role).ToListAsync();
                //return _context.Users.Include(x => x.Role).ToListAsync();
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "{Repo} GetAllWithRoleAsync error", typeof(User));

                return new List<User>();
            }
        }
        public IEnumerable<User> GetAllWithRoleWithSourcePartner()
        {
            try
            {
                return dbSet.Include(x => x.Role).Include(x => x.SourcePartner).ToList();
                //return _context.Users.Include(x => x.Role).Include(x => x.SourcePartner).ToList();
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "{Repo} GetAllWithRoleWithSourcePartner error", typeof(User));

                return new List<User>();
            }
        }
        public async ValueTask<IEnumerable<User>> GetAllWithRoleWithSourcePartnerAsync()
        {
            try
            {
                return await dbSet.Include(x => x.Role).Include(x => x.SourcePartner).ToListAsync();
                //return _context.Users.Include(x => x.Role).Include(x => x.SourcePartner).ToListAsync();
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "{Repo} GetAllWithRoleWithSourcePartnerAsync error", typeof(User));

                return new List<User>();
            }
        }
        public User GetUserWithRoleAndInvestment(Guid id)
        {
            return dbSet.Where(x => x.IsActive && x.Id == id).Include(x => x.Role).Include(x => x.InvestmentModels).FirstOrDefault();
        }
        public User GetUserWithRole(string email)
        {
            return dbSet.Where(x => x.IsActive && x.EmailAddress == email).Include(x => x.Role).FirstOrDefault();
        }
        public User GetUserWithRoleSourcePartner(string email)
        {
            return dbSet.Where(x => x.IsActive && x.EmailAddress == email).Include(x => x.Role).Include(x => x.SourcePartner).FirstOrDefault();
        }

        public IEnumerable<User> GetUserWithReferalId(string refid)
        {
            try
            {
                return dbSet.Where(x => x.ReferredById == refid).Include(x => x.InvestmentModels).ToList();
                //return _context.Users.Where(x => x.ReferredById == refid).ToList();
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "{Repo} GetUserWithReferalId error", typeof(User));

                return new List<User>();
            }
        }
        public async ValueTask<IEnumerable<User>> GetUserWithReferalIdAsync(string refid)
        {
            try
            {
                return await dbSet.Where(x => x.ReferredById == refid).ToListAsync();
                //return _context.Users.Where(x => x.ReferredById == refid).ToListAsync();
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "{Repo} GetUserWithReferalIdAsync error", typeof(User));

                return new List<User>();
            }
        }
    }
}

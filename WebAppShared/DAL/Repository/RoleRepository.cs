using WebAppShared.DAL.Interface;
using WebAppShared.ModelsApi;

namespace WebAppShared.DAL.Repository
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(AppDataContext context) : base(context)
        {

        }
    }
}

using WebAppShared.DAL.Interface;
using WebAppShared.ModelsApi;

namespace WebAppShared.DAL.Repository
{
    public class DeletedUserRepository : Repository<DeletedUser>, IDeletedUserRepository
    {
        public DeletedUserRepository(AppDataContext context) : base(context)
        {
        }

    }
}

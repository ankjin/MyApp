using WebAppShared.DAL.Interface;
using WebAppShared.Models;

namespace WebAppShared.DAL.Repository
{
    public class SourcePartnerRepository : Repository<SourcePartner>, ISourcePartnerRepository
    {
        public SourcePartnerRepository(AppDataContext context) : base(context)
        {

        }
    }
}

using WebAppShared.DAL.Interface;
using WebAppShared.Models;

namespace WebAppShared.DAL.Repository
{
    public class NotificationModelRepository : Repository<NotificationModel>, INotificationModelRepository
    {
        public NotificationModelRepository(AppDataContext context) : base(context)
        {

        }
    }
}

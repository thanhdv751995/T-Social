using Extention.Management.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Notifications
{
    public class NotificationAppService: ManagementAppService, INotificationAppService
    {
        private readonly INotificationsRepository _notificationsRepository;
        private readonly IClientRepository _clientRepository;
        private readonly NoticationManager _notificationManager;
        public NotificationAppService(
            INotificationsRepository notificationsRepository,
            IClientRepository clientRepository,
            NoticationManager notificationManager
          )
        {
            _clientRepository = clientRepository;
            _notificationsRepository = notificationsRepository;
            _notificationManager = notificationManager;
        }
        public async Task<NotificationDto> CreateAsync(CreateNotificationDto createNotificationDto)
        {
            bool isDuplicate = _notificationsRepository.Any(x => x.IdUser == createNotificationDto.IdUser && x.Content == createNotificationDto.Content);
            if (!isDuplicate)
            {
               var notification = _notificationManager.CreateAsync(
                createNotificationDto.IdUser,
                createNotificationDto.Content,
                createNotificationDto.UrlAvatar,
                createNotificationDto.Time,
                createNotificationDto.Href);
                await _notificationsRepository.InsertAsync(notification);
                return ObjectMapper.Map<Notification, NotificationDto>(notification);
            }
            else
            {
                return null;
            }    

        }
        //public async Task<List<NotificationDto>> GetListAsync(string userName)
        //{
        //   // var client = await _clientRepository.GetAsync(x => x.UserName == userName);
        //    var notifications = await _notificationsRepository.GetListAsync(x=>x.IdUser == userName);
        //    var nfMap = ObjectMapper.Map<List<Notification>, List<NotificationDto>>(notifications);

        //    //nfMap.ForEach(x =>
        //    //{
        //    //    x.UserId = client.Id;
        //    //    x.GroupTypeName = EnumAppService.GetNameEnum<GroupType>(x.GroupType);
        //    //    x.UserName = client.UserName;
        //    //});

        //    return nfMap;
        //}
    }
}

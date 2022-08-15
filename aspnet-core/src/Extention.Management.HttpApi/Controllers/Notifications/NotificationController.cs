using Extention.Management.Notifications;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Controllers.Notifications
{
    [Microsoft.AspNetCore.Mvc.Route("api/notification")]
    public class NotificationController : ManagementController
    {
        private readonly NotificationAppService _notificationAppService;

        public NotificationController(NotificationAppService notificationAppService)
        {
            _notificationAppService = notificationAppService;
        }
        [HttpPost("Create")]
        public async Task<NotificationDto> CreateAsync([FromBody]CreateNotificationDto createNotificationDto)
        {
          var notification=   await _notificationAppService.CreateAsync(createNotificationDto);
         return notification;
        }
    }
}

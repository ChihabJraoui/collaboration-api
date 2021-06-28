using Collaboration.ShareDocs.Application.Commands.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Api.Controllers
{
    [Authorize]
    [Route("api/notifications")]
    public class NotificationsController: BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetNotifications()
        {
            var result = await this.Mediator.Send(new GetNotificationCommand());
            return FormatResponseToActionResult(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{notificationId:Guid}")]
        public async Task<IActionResult> ReadNotification(Guid notificationId)
        {
            var result = await this.Mediator.Send(new ReadNotificationCommand() { NotificationId = notificationId});
            return FormatResponseToActionResult(result);
        }
    }
}

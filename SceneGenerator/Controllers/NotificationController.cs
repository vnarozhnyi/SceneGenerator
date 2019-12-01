using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SceneGenerator.Controllers
{
    public class NotificationController
    {
        public void SendMessage(string message)
        {
            GlobalHost
            .ConnectionManager
            .GetHubContext<NotificationHub>()
            .Clients.sendMessage(message);
        }
    }
}
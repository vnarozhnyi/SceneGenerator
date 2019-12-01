using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebSockets.Internal;
using Microsoft.Extensions.Logging;
using SceneGenerator.DataRepository;
using SceneGenerator.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SceneGenerator.Controllers
{
    [ApiController]
    [Route("api/settings")]
    public class SettingsController : ControllerBase
    {
        private readonly ILogger<MetaVideoController> _logger;
        private readonly DataProvider _dataProvider;
        private readonly string _version;

        public SettingsController(ILogger<MetaVideoController> logger)
        {
            _logger = logger;
            _version = Constants.IncVersion.ToString(CultureInfo.InvariantCulture);
            _dataProvider = DataProvider.GetInstance();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<NotificationModel> GetNotificationMask() =>
            Ok(new NotificationModel
            {
                NotificationMask = _dataProvider.FindNotificationMaskByUserId(GetUserId()).NotificationMask
            });

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<NotificationModel> SetNotificationMask([FromQuery] int newNotificationMask)
        {
            if (!Enum.IsDefined(typeof(NotificationMasks), newNotificationMask))
            {
                return BadRequest("Notification mask is not correct. Must be formed from " + GetMaskValues());
            }

            var notificationModel = new NotificationModel { NotificationMask = newNotificationMask };

            return Ok(_dataProvider.GetDataSet<NotificationModel>(_dataProvider.SettDataSet(notificationModel)));
        }

        private long GetUserId() => Convert.ToInt64(new IdentityUser().Id);

        private int[] GetMaskValues() => new[]
        {
            (int) NotificationMasks.NONE,
            (int) NotificationMasks.EMAIL,
            (int) NotificationMasks.PUSH
        };
    }

    [Flags] enum NotificationMasks { NONE = 0, EMAIL = 1, PUSH = 2 }
}
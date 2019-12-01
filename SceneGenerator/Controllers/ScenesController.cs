using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SceneGenerator.DataRepository;
using SceneGenerator.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SceneGenerator.Controllers
{
    [ApiController]
    [Route("api/scenes")]
    public class ScenesController : ControllerBase
    {
        private readonly ILogger<ScenesController> _logger;
        public ScenesProvider ScenesProvider = new ScenesProvider();

        public ScenesController(ILogger<ScenesController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AddScene()
        {
            ScenesProvider.AddScene(new GenerationModel());
            return Ok(ScenesProvider.GetUsersScenes("1"));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<GenerationModel[]> GetUserScenes(string userId)
        {
            return Ok(ScenesProvider.GetUsersScenes(userId));
        }
    }
}
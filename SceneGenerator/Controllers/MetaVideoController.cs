using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebSockets.Internal;
using Microsoft.Extensions.Logging;
using SceneGenerator.DataRepository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SceneGenerator.Controllers
{
    [ApiController]
    [Route("api/metavideo")]
    public class MetaVideoController : ControllerBase
    {
        private readonly ILogger<MetaVideoController> _logger;
        private readonly DataProvider _dataProvider;
        private readonly string _version;

        public MetaVideoController(ILogger<MetaVideoController> logger)
        {
            _logger = logger;
            _version = Constants.IncVersion.ToString(CultureInfo.InvariantCulture);
            _dataProvider = DataProvider.GetInstance();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<BaseResponse> PostMetadata(IFormFile videoFile, IFormFile jsonMetadata)
        {
            if (videoFile == null || jsonMetadata == null)
                return BadRequest(Constants.ResponseBadRequestLength);

            if (videoFile.ContentType != Constants.VideoFileMimeType || jsonMetadata.ContentType != Constants.MetadataMimeType)
                return BadRequest(Constants.ResponseBadRequestType);

            var videoFileSizeRange = Enumerable.Range(Constants.MinVideoFileLengthMb.MbToBytes(), Constants.MaxVideoFileLengthMb.MbToBytes());
            var metadataFileSizeRange = Enumerable.Range(Constants.MinMetadataFileLengthMb.MbToBytes(), Constants.MaxMetadataFileLengthMb.MbToBytes());

            if (!CheckFileLength(videoFile, videoFileSizeRange) || !CheckFileLength(jsonMetadata, metadataFileSizeRange)
            ) return BadRequest(Constants.ResponseBadRequestLength);

            var task = ProcessVideoFile(videoFile, jsonMetadata);

            return Ok(new MetaVideoResponse(_version, new MetaVideoData(Constants.ResponseMessageOk + task)));
        }

        private static bool CheckFileLength(IFormFile file, IEnumerable<int> range) => range.Contains((int)file.Length);

        private async Task<GenerationResponse> ProcessVideoFile(IFormFile videoFile, IFormFile jsonMetadata)
        {
            var id = _dataProvider.SettDataSet(new MetaVideoData(Constants.ResponseMessageOk));

            var generationModel = _dataProvider.GetDataSet<GenerationModel>(id);

            return new GenerationResponse(_version, new GenerationResponseData
            {
                Id = generationModel.Id,
                Name = generationModel.Name,
                Time = generationModel.Time
            });
        }
    }
}
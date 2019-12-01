using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SceneGenerator.Models
{
    public class MetaVideoDataModel
    {
        public string Message { get; set; }

        public void MetaVideoData(string message)
        {
            Message = message;
        }
    }
}
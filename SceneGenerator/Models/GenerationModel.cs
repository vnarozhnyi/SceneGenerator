using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SceneGenerator.Models.DataModel
{
    public class GenerationModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public DateTime Time { get; set; }

        public string FilePath { get; set; }
    }
}
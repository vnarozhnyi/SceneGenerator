using SceneGenerator.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SceneGenerator.DataRepository
{
    public class ScenesProvider
    {
        public List<GenerationModel> Scenes = new List<GenerationModel>() { new GenerationModel() };

        public bool AddScene(GenerationModel scene)
        {
            Scenes.Add(scene);
            return true;
        }

        public List<GenerationModel> GetUsersScenes(string id)
        {
            return Scenes;
        }
    }
}

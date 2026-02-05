using System;
using System.Collections.Generic;

using Task.Core;

using UnityEngine;


namespace Task.Data
{
    [Serializable]
    public class PlayerData
    {
        public List<LevelData> completedLevels;
        public int lastCompletedLevelIndex = -1;

        public PlayerData()
        {
            completedLevels = new List<LevelData>();
        }
        public LevelData GetLevelDataBybuildIndex(int buildIndex)
        {
            return completedLevels.Find((ld) => ld.buildIndex == buildIndex);
        }
        public void LevelCompleted(SceneReference reference, int score)
        {
            //TODO: switch to dictionary(with newtonsoft.json) lookup or sort list by buildIndex for a binary search
            LevelData levelData = completedLevels.Find((levelData) => levelData.buildIndex == reference.buildIndex);
            if(levelData != null)
            {
                int index = completedLevels.IndexOf(levelData);
                if(completedLevels[index].score < score)
                {
                    completedLevels[index].score = score;
                }
            }
            else
            {
                completedLevels.Add(new LevelData(reference, score));
                int orderIndex = Loader.Instance.SceneReferences.GetLevelIndex(reference.buildIndex);
                if(orderIndex > lastCompletedLevelIndex )
                {
                    lastCompletedLevelIndex = orderIndex;
                }
            }
            
        }
    }
    [Serializable]
    public class LevelData
    {

        public int buildIndex = -1;
        public string levelName;
        public int score = -1;

        
        public LevelData(SceneReference sceneReference, int score)
        {
            buildIndex = sceneReference.buildIndex;
            levelName = sceneReference.sceneName;
            this.score = score;
        }

    }
}

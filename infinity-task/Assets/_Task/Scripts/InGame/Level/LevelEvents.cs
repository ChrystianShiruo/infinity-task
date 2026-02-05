using System;
using Task.Data;
using Task.Level.Part;

namespace Task.InGame
{
    public class LevelEvents
    {
        public Action<BaseLevelElement> OnPartClicked;
        public Action<BaseLevelElement> OnPartChanged;
        /// <summary>
        /// (Level SceneReference, Score)
        /// </summary>
        public Action<SceneReference, int> OnLevelCompleted;
    }
}
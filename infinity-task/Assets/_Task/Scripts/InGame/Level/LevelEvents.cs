using System;

using Task.Level.Part;

using UnityEngine;

namespace Task.InGame
{
    public class LevelEvents
    {
        
        public Action<BaseLevelElement> OnPartClicked;
        public Action<BaseLevelElement> OnPartChanged;
        public Action OnLevelCompleted;



    }
}
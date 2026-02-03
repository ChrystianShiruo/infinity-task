using System;

using UnityEngine;

namespace Task.InGame
{
    public class LevelEvents : MonoBehaviour
    {
        
        public Action<bool> OnPartClicked;
        public Action<bool> OnLevelCompleted;



    }
}
using Task.InGame.Managers;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Task {
    public class MenuController : MonoBehaviour
    {

        private void Start()
        {
            if(LevelManager.Instance != null)
            {
                LevelManager.Instance.LoadLevel(0);
            }
        }
    }
}
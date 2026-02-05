using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Task.Data;
using System.IO;
using Task.InGame;
using Task.InGame.Managers;

namespace Task.Core
{
    public class DataManager : MonoBehaviour
    {
        public static PlayerData PlayerData { get => _playerData; }

        private static PlayerData _playerData = null;

        public static DataManager instance = null;


        private static string _filename = "PlayerData";

        public static void SaveJson()
        {
            string data = JsonUtility.ToJson(_playerData);
            Debug.Log(data);
            System.IO.File.WriteAllText($"{Application.persistentDataPath}/{_filename}.json", data);
        }

        public PlayerData LoadJson()
        {
            string path = $"{Application.persistentDataPath}/{_filename}.json";
            if(!File.Exists(path))
            {
                Debug.Log("!File.Exists");
                CreatePlayerData();
            }
            else
            {
                string data = File.ReadAllText(path);
                Debug.Log($"SaveData: {data}");
                if(data == string.Empty)
                {
                    CreatePlayerData();
                }
                _playerData = JsonUtility.FromJson<PlayerData>(data);

            }
            Debug.Log($"Data path: {path}");
            return PlayerData;
        }

        private void Awake()
        {
            if(instance != null)
            {
                Destroy(this);
                return;
            }
            instance = this;
            DontDestroyOnLoad(this);
            _playerData = LoadJson();

            //_path = Application.persistentDataPath;
        }
        private void Start()
        {
            if(LevelManager.Instance != null)
            {
                LevelManager.Instance.LevelEvents.OnLevelCompleted += _playerData.LevelCompleted;
            }
        }
        private void OnDestroy()
        {
            SaveJson();
        }

        private void CreatePlayerData()
        {
            _playerData = new PlayerData();
            SaveJson();
        }
    }
}
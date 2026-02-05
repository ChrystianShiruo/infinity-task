using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Task.Data.Visual;
using Task.Level.Part;
using Task.Core;
using Task.Data;
using System.Linq;

namespace Task.InGame.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance { get; private set; }
        public LevelEvents LevelEvents { get => _levelEvents; }
        public LevelVisualSettings LevelVisualData { get => _levelVisualData; }
        public SceneReference CurrentLevel { get => _currentLevel; }

        [SerializeField] private LevelVisualSettings _levelVisualData;
        [SerializeField] private Loader _loader;
        [SerializeField] private float _levelCompleteDelay = 2f;


        private LevelEvents _levelEvents;
        private int _loadedLevelReferenceIndex = -1;


        private Dictionary<Vector2Int, List<BaseLevelElement>> _connectionDictionary;//Stores every element that currently connects to a specific point;
        private Vector2Int[] _energySourcePositions;
        private List<BaseLevelElement> _elementsList;//Wether connections are on/off
        private LightBulb[] _energyTargets; // we need to power up these
        private SceneReference _currentLevel;
        private WaitForSeconds _levelCompleteWaitForSeconds;
        public void LoadLevel(int buildIndex)
        {
            int i = _loader.SceneReferences.GetLevelIndex(buildIndex);
            if(i == -1)
            {
                Debug.LogError($"could not find buildIndex {buildIndex} on _orderedLevelScenes!");
                return;
            }
            StartCoroutine(LoadNewLevel(i));

        }

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(this);
                return;
            }
            Instance = this;
            _levelEvents = new LevelEvents();
            DontDestroyOnLoad(this);
            _levelCompleteWaitForSeconds = new WaitForSeconds(_levelCompleteDelay);
        }

        private void OnEnable()
        {
            LevelEvents.OnPartChanged += UpdateConnections;
            LevelEvents.OnLevelCompleted += LoadNextLevel;
        }

        private void OnDisable()
        {
            LevelEvents.OnPartChanged -= UpdateConnections;
            LevelEvents.OnLevelCompleted -= LoadNextLevel;
        }


        private void LoadNextLevel(object _, int _1)
        {
            StartCoroutine(LoadNewLevel(_loadedLevelReferenceIndex + 1, true));
        }

        private IEnumerator LoadNewLevel(int i, bool delay = false)
        {
            if(delay)
            {
                yield return _levelCompleteWaitForSeconds;
            }
            if(_loadedLevelReferenceIndex != -1)
            {
                Debug.Log($"Unload Level of build id {_loadedLevelReferenceIndex}");
                yield return _loader.UnloadScene(_loader.SceneReferences.OrderedLevelScenes[_loadedLevelReferenceIndex]);
            }
            if(i >= _loader.SceneReferences.OrderedLevelScenes.Count || i < 0)
            {
                //TODO: handle last level completion
                yield return _loader.LoadMenu();
                yield break;
            }

            Debug.Log($"Load Level {_loader.SceneReferences.OrderedLevelScenes[i].sceneName} of build id {_loader.SceneReferences.OrderedLevelScenes[i].buildIndex}");
            yield return _loader.LoadScene(_loader.SceneReferences.OrderedLevelScenes[i]);
            _currentLevel = _loader.SceneReferences.OrderedLevelScenes[i];
            _loadedLevelReferenceIndex = i;
            Debug.Log(_loadedLevelReferenceIndex);

            var levelElements = FindObjectsByType<BaseLevelElement>(FindObjectsSortMode.None);
            _elementsList = new List<BaseLevelElement>();
            _connectionDictionary = new Dictionary<Vector2Int, List<BaseLevelElement>>();

            foreach(var element in levelElements)//populate connections dictionary
            {
                var elementConnections = element.UpdateConnectorPositions();
                _elementsList.Add(element);
                element.TogglePower(false);

                foreach(var connectionPosition in elementConnections)
                {
                    if(!_connectionDictionary.ContainsKey(connectionPosition))
                    {
                        _connectionDictionary.Add(connectionPosition, new List<BaseLevelElement>());
                    }
                    _connectionDictionary[connectionPosition].Add(element);
                }
            }

            var energySources = FindObjectsByType<EnergySource>(FindObjectsSortMode.None);//TODO: cache references
            _energySourcePositions = new Vector2Int[energySources.Length];
            for(int j = 0; j < energySources.Length; j++)
            {
                _energySourcePositions[j] = (Vector2Int.RoundToInt(energySources[j].transform.position));
                energySources[j].transform.position = (Vector2)Vector2Int.RoundToInt(energySources[j].transform.position);
                energySources[j].TogglePower(true);
            }

            _energyTargets = FindObjectsByType<LightBulb>(FindObjectsSortMode.None);//TODO: cache references

            EvaluateLevel();
        }

        private void UpdateConnections(BaseLevelElement element)
        {
            foreach(var connectionPosition in element.ConnectorPositions)
            {
                if(_connectionDictionary.TryGetValue(connectionPosition, out var connectedElements))
                {
                    if(connectedElements.Remove(element))
                    {
                        if(connectedElements.Count == 0)
                        {
                            _connectionDictionary.Remove(connectionPosition);
                        }
                    }
                }
            }
            element.UpdateConnectorPositions();

            foreach(var connectionPosition in element.ConnectorPositions)
            {
                if(!_connectionDictionary.ContainsKey(connectionPosition))
                {
                    _connectionDictionary.Add(connectionPosition, new List<BaseLevelElement>());
                }
                _connectionDictionary[connectionPosition].Add(element);
            }

            EvaluateLevel();
        }

        /// <summary>
        /// Check if we completed the level
        /// </summary>
        private void EvaluateLevel()
        {
            ResetPower();
            RunPowerUp();

            bool success = true;
            int remainingTargets = 0;

            foreach(var item in _energyTargets)
            {
                if(!item.Powered)
                {
                    remainingTargets++;
                    success = false;
                }
            }

            if(success)
            {
                LevelEvents.OnLevelCompleted?.Invoke(CurrentLevel, 3);
            }
        }

        private void ResetPower()
        {
            foreach(var item in _elementsList)
            {
                item.TogglePower(false);
            }
        }
        private void RunPowerUp()
        {
            Queue<Vector2Int> nodesCheck = new Queue<Vector2Int>();

            foreach(var sourcePos in _energySourcePositions)
            {
                nodesCheck.Enqueue(sourcePos);
            }
            while(nodesCheck.Count > 0)
            {
                Vector2Int currentPos = nodesCheck.Dequeue();

                if(_connectionDictionary.TryGetValue(currentPos, out var elementsAtPos))
                {
                    foreach(var element in elementsAtPos)
                    {
                        if(!element.Powered)
                        {
                            element.TogglePower(true);
                            foreach(var pin in element.ConnectorPositions)
                            {
                                nodesCheck.Enqueue(pin);
                            }
                        }
                    }
                }
            }
        }
    }
}
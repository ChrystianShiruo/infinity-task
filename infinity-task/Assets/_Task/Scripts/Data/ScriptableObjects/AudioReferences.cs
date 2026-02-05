using System;

using UnityEngine;


namespace Task.Data.Audio
{
    [CreateAssetMenu(fileName = "AudioReferences", menuName = "Data/Audio/AudioReferences")]
    public class AudioReferences : ScriptableObject
    {
        public AudioReference sfx;
        public AudioReference music;

    }
    [Serializable]
    public class AudioReference
    {
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 1f;
        [Tooltip("start time in seconds")]
        public float startTime = 0f;
        //public float endTime = 1f;

    }
}

using System;
using System.Collections;
using System.Collections.Generic;

using Task.Data.Audio;
using Task.InGame.Managers;
using Task.Level.Part;

using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioReferences _audioReferences;
    [SerializeField] private AudioSource _musicAudioSource;
    [SerializeField] private GameObject _sfxSourcePrefab;

    private Queue<KeyValuePair<AudioSource, WaitWhile>> _availableSfxAudiosources = new Queue<KeyValuePair<AudioSource, WaitWhile>>();

    public void PlaySfx(AudioReference audioReference)
    {
        var audiosource = GetAvailableSfxSource();
        StartCoroutine(PlaySfxRoutine(audiosource, audioReference));
    }
    public void PlaySfx()
    {
        PlaySfx(_audioReferences.sfx);
    }

    public void PlayMusic(AudioReference audioReference)
    {
        StartCoroutine(PrepareAndPlayAudio(new KeyValuePair<AudioSource, WaitWhile>(_musicAudioSource,null), audioReference));
    }


    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        CreateSfxSources(3);
    }
    private void Start()
    {
        PlayMusic(_audioReferences.music);
    }
    private void OnEnable()
    {
        if(LevelManager.Instance != null)
        {
            LevelManager.Instance.LevelEvents.OnPartChanged += OnPartChangedAudioFeedback;
        }
    }
    private void OnDisable()
    {
        if(LevelManager.Instance != null)
        {
            LevelManager.Instance.LevelEvents.OnPartChanged -= OnPartChangedAudioFeedback;
        }
    }
    private void OnPartChangedAudioFeedback(BaseLevelElement element)
    {
        PlaySfx();
    }

    private KeyValuePair<AudioSource, WaitWhile> GetAvailableSfxSource()
    {
        if(_availableSfxAudiosources.Count == 0)
        {
            CreateSfxSources(2);
        }
        return _availableSfxAudiosources.Dequeue();
    }

    private void CreateSfxSources(int count)
    {
        for(int i = 0; i < count; i++)
        {
            var audiosource = Instantiate(_sfxSourcePrefab, transform).GetComponent<AudioSource>();

            StartCoroutine(LoadClip(audiosource, _audioReferences.sfx));
            var availableAudioSource = new KeyValuePair<AudioSource, WaitWhile>(audiosource, new WaitWhile(()=>audiosource.isPlaying));
            _availableSfxAudiosources.Enqueue(availableAudioSource);
        }
    }
    private void Play(AudioSource audioSource, AudioReference audioReference)
    {
        audioSource.volume = audioReference.volume;
        audioSource.time = audioReference.startTime;
        audioSource.Play();
        Debug.Log("play");
    }
    private IEnumerator PlaySfxRoutine(KeyValuePair<AudioSource, WaitWhile> audioSource, AudioReference audioReference)
    {
        if(audioSource.Key.clip == audioReference.clip && audioSource.Key.clip.loadState == AudioDataLoadState.Loaded)
        {
            //audiosource.Play();
            Play(audioSource.Key, audioReference);
        }
        else
        {
            yield return PrepareAndPlayAudio(audioSource, audioReference);
        }
        yield return audioSource.Value;
    }
    private IEnumerator PrepareAndPlayAudio(KeyValuePair<AudioSource, WaitWhile> audioSource, AudioReference audioReference)
    {
        yield return LoadClip(audioSource.Key, audioReference);

        if(audioSource.Key.clip.loadState == AudioDataLoadState.Loaded)
        {
            Play(audioSource.Key, audioReference);
            //audioSource.Play();
        }
        else if(audioSource.Key.clip.loadState == AudioDataLoadState.Failed)
        {
            Debug.LogError($"failed to load audioclip {audioSource.Key.clip} onto {audioSource.Key.gameObject.name}");
        }
    }

    private IEnumerator LoadClip(AudioSource audioSource, AudioReference audioReference)
    {

        audioSource.clip = audioReference.clip;
        audioSource.clip.LoadAudioData();
        yield return new WaitUntil(() => audioSource.clip.loadState != AudioDataLoadState.Unloaded &&
        audioSource.clip.loadState != AudioDataLoadState.Loading);

        audioSource.time = audioReference.startTime;
    }
}

using System;
using System.Collections.Generic;

using Task.Core;
using Task.Data;

using UnityEngine;


namespace Task.SdkImplementation
{
    public class AmplitudeEvents : MonoBehaviour
    {
        Amplitude amplitude;
        void Awake()
        {
            amplitude = Amplitude.getInstance();
            amplitude.setServerZone(AmplitudeServerZone.EU);
            amplitude.logging = true;
            amplitude.trackSessionEvents(true);
            amplitude.init("1eb3d130d32cb8e515c2742541295bf5");
        }
        private void OnEnable()
        {
            Loader.Instance.LevelEvents.OnLevelCompleted += LevelCompletedEvent;
        }
        private void OnDisable()
        {
            Loader.Instance.LevelEvents.OnLevelCompleted -= LevelCompletedEvent;
        }
        private void Start()
        {
            amplitude.logEvent("App Initialized");

        }
        private void LevelCompletedEvent(SceneReference reference, int score)
        {
            Dictionary<string, object> eventProps = new Dictionary<string, object>();

            eventProps.Add("Level Name", reference.sceneName);
            eventProps.Add("Score", score);

            amplitude.logEvent("LevelCompleted", eventProps);

        }

        private void AmplitudeEvent()
        {
            amplitude.logEvent("Sign Up");
        }
    }
}
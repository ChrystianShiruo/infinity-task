using UnityEngine;
using System.Collections;
// using com.amplitude.unity.plugins; // Uncomment if needed

public class AmplitudeTest : MonoBehaviour
{
    private Amplitude _amplitude;

    //IEnumerator Start()
    //{
    //    // 1. Get Instance
    //    _amplitude = Amplitude.getInstance();

    //    // 2. Setup Debugging
    //    _amplitude.setServerUrl("https://api2.amplitude.com");
    //    _amplitude.logging = true;
    //    _amplitude.trackSessionEvents(true);

    //    // 3. Set a short upload period (DEFAULT is 30s, change to 5s for testing)
    //    _amplitude.setEventUploadPeriodSeconds(5);

    //    // 4. Initialize
    //    _amplitude.init("1eb3d130d32cb8e515c2742541295bf5");
    //    Debug.Log("Amplitude: Initializing...");

    //    // 5. WAIT for Session ID (Vital step!)
    //    yield return new WaitForSeconds(1.0f);

    //    // 6. Log Event
    //    _amplitude.logEvent("TEST_EVENT_CODE_ONLY");
    //    Debug.Log("Amplitude: Event Queued");

    //    // 7. FORCE UPLOAD
    //    yield return new WaitForSeconds(0.5f);
    //    _amplitude.uploadEvents();
    //    Debug.Log("Amplitude: Upload Triggered Manually");
    //}

    //// Optional: Ensure flush happens if you stop play mode abruptly
    //void OnApplicationQuit()
    //{
    //    if(_amplitude != null)
    //    {
    //        _amplitude.uploadEvents();
    //    }
    //}

    private void Start()
    {
        Amplitude amplitude = Amplitude.getInstance();
        amplitude.setServerUrl("https://api2.amplitude.com");
        amplitude.logging = true;
        amplitude.trackSessionEvents(true);
        amplitude.init("1eb3d130d32cb8e515c2742541295bf5");

        amplitude.logEvent("Sign Up");
    }
}
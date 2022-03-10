using UnityEngine;

[RequireComponent(typeof(SnapAdjustController))]
public class SnapAdjustSounds : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField]
    [Tooltip("Sound to play when interaction with slider starts")]
    private AudioClip interactionStartSound = null;
    [SerializeField]
    [Tooltip("Sound to play when interaction with slider ends")]
    private AudioClip interactionEndSound = null;

    [Header("Tick Notch Sounds")]

    [SerializeField]
    [Tooltip("Whether to play 'tick tick' sounds as the slider passes notches")]
    private bool playTickSounds = true;

    [SerializeField]
    [Tooltip("Sound to play when slider passes a notch")]
    private AudioClip passNotchSound = null;

    [SerializeField]
    private float highPitch = 1.25f;

    [SerializeField]
    private float lowPitch = 0.75f;

    [SerializeField]
    private float minSecondsBetweenTicks = 0.01f;


    #region Private members
    private SnapAdjustController slider;

    // Play sound when passing through slider notches
    private float accumulatedDeltaSliderValue = 0;
    private float lastSoundPlayTime;

    private AudioSource grabReleaseAudioSource = null;
    private AudioSource passNotchAudioSource = null;
    #endregion
    
    private Vector3 lastPosition;

    public float minSpeed = 0f;
    public float maxSpeed = 1f;
    
    [Range(0, 1)]
    public float volume = 1f;

    [Range(0, 1)]
    public float slowVolume = 1f;
    [Range(0, 1)]
    public float fastVolume = 0f;

    private void Start()
    {
        if (grabReleaseAudioSource == null)
        {
            grabReleaseAudioSource = gameObject.AddComponent<AudioSource>();
        }
        if (passNotchAudioSource == null)
        {
            passNotchAudioSource = gameObject.AddComponent<AudioSource>();
        }
        slider = GetComponent<SnapAdjustController>();
        slider.OnInteractionStarted.AddListener(OnInteractionStarted);
        slider.OnInteractionEnded.AddListener(OnInteractionEnded);
        slider.OnValueUpdated.AddListener(OnValueUpdated);
    }

    private void OnValueUpdated()
    {
        if (playTickSounds && passNotchAudioSource != null && passNotchSound != null)
        {
            Vector3 change = slider.snapAdjustTarget.position - lastPosition;
            lastPosition = slider.snapAdjustTarget.position;
            var now = Time.timeSinceLevelLoad;
            if (change.magnitude > 0f && now - lastSoundPlayTime > minSecondsBetweenTicks)
            {
                float snapAmnt = Mathf.Pow(10, slider.snapSigFigs);
                float speedT = Mathf.InverseLerp(minSpeed, maxSpeed, change.magnitude/snapAmnt/(now - lastSoundPlayTime));
                passNotchAudioSource.pitch = Mathf.Lerp(lowPitch, highPitch, speedT);
                float volumeCoef = Mathf.Lerp(slowVolume, fastVolume, speedT);
                if (passNotchAudioSource.isActiveAndEnabled)
                {
                    passNotchAudioSource.PlayOneShot(passNotchSound, volume*volumeCoef);
                }

                accumulatedDeltaSliderValue = 0;
                lastSoundPlayTime = now;
            }
        }
    }

    private void OnInteractionEnded()
    {
        if (interactionEndSound != null && grabReleaseAudioSource != null && grabReleaseAudioSource.isActiveAndEnabled)
        {
            grabReleaseAudioSource.PlayOneShot(interactionEndSound, volume);
        }
    }

    private void OnInteractionStarted()
    {
        if (interactionStartSound != null && grabReleaseAudioSource != null && grabReleaseAudioSource.isActiveAndEnabled)
        {
            grabReleaseAudioSource.PlayOneShot(interactionStartSound, volume);
        }
        lastPosition = slider.snapAdjustTarget.position;
    }
}


using Oculus.Interaction;
using UnityEngine;
using UnityEngine.Audio;

// Could just be seperate components tbh but whatever this is easier to write up fast
public enum AudioPassType
{
    LowPass,
    HighPass,
    MidPass,
}

public class LowMidHighPass : MonoBehaviour
{
    [SerializeField] private AudioPassType audioPassType;
    
    [SerializeField] [Range(20, 20000)] private float lowPassMinimum = 20.0f;
    [SerializeField] [Range(20, 20000)] private float lowPassMaximum = 20000.0f;

    [SerializeField] [Range(20, 20000)] private float highPassMinimum = 20.0f;
    [SerializeField] [Range(20, 20000)] private float highPassMaximum = 20000.0f;

    [SerializeField] private AudioSource associatedAudioSource = null;
    [SerializeField] private string audioMixerLowpassParameter = "";
    [SerializeField] private string audioMixerHighpassParameter = "";
    private AudioMixer audioMixer = null;

    private OneGrabRotateTransformer grabTransformer;

    private void Start()
    {
        audioMixer = associatedAudioSource.outputAudioMixerGroup.audioMixer;

        grabTransformer = GetComponent<OneGrabRotateTransformer>();
    }

    private void Update()
    {
        switch (audioPassType)
        {
            case AudioPassType.LowPass:
                HandleLowPass();
                break;
            case AudioPassType.HighPass:
                HandleHighPass();
                break;
            case AudioPassType.MidPass:
                HandleMidPass();
                break;
        }
    }

    private void HandleLowPass()
    {
        audioMixer.SetFloat(audioMixerLowpassParameter,((lowPassMaximum - lowPassMinimum) * GetNormalizedAngle(transform.eulerAngles.y, grabTransformer.Constraints.MinAngle.Value, grabTransformer.Constraints.MaxAngle.Value)) + lowPassMinimum);
    }
    
    private void HandleHighPass()
    {
        audioMixer.SetFloat(audioMixerHighpassParameter,((highPassMinimum - highPassMaximum) * GetNormalizedAngle(transform.eulerAngles.y, grabTransformer.Constraints.MinAngle.Value, grabTransformer.Constraints.MaxAngle.Value)) + highPassMaximum);
    }
    
    private void HandleMidPass()
    {
        float normalizedValue = GetNormalizedAngle(transform.eulerAngles.y, grabTransformer.Constraints.MinAngle.Value, grabTransformer.Constraints.MaxAngle.Value);

        audioMixer.SetFloat(audioMixerLowpassParameter,((lowPassMaximum - lowPassMinimum) * normalizedValue) + lowPassMinimum);
        audioMixer.SetFloat(audioMixerHighpassParameter,((highPassMinimum - highPassMaximum) * (1-normalizedValue)) + highPassMaximum);
    }

    // TODO: Calculations require a negative number in here so lets leave this as is for now thenf ix it later    
    private float GetNormalizedAngle(float currentAngle, float minAngle, float maxAngle)
    {
        if (Mathf.Sign(minAngle) == Mathf.Sign(maxAngle))
        {
            return (Mathf.Clamp(currentAngle, minAngle, maxAngle) - minAngle) / (maxAngle - minAngle);
        }

        // Shift All Angles To 0 to Positive Range To Simplify Things
        float shiftedAngle = (currentAngle > 180 ? currentAngle - 360 : currentAngle) + Mathf.Abs(minAngle); // Just by default as we shift the negative one over to the positive
        float range = Mathf.Abs(maxAngle) + Mathf.Abs(minAngle);

        return shiftedAngle / range;
    }
}

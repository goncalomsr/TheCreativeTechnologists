using Oculus.Interaction;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeHandler : MonoBehaviour
{
    private OneGrabTranslateTransformer translateTransformer;
    private AudioMixer mixer;
    public string MusicPlayerTag; // "music1Volume"
    public string MixerParameter;

    private Vector3 initialPosition = Vector3.zero;
    private Vector3 finalPosition = Vector3.zero;


    private void Start()
    {
        translateTransformer = gameObject.GetComponent<OneGrabTranslateTransformer>();
        mixer = GameObject.FindGameObjectWithTag(MusicPlayerTag)?.GetComponent<AudioSource>().outputAudioMixerGroup.audioMixer;

        // Were only using relative for now
        if (translateTransformer.Constraints.ConstraintsAreRelative)
        {
            Vector3 minOffset = new Vector3(
                translateTransformer.Constraints.MinX.Value,
                translateTransformer.Constraints.MinY.Value,
                translateTransformer.Constraints.MinZ.Value);

            Vector3 maxOffset = new Vector3(
                translateTransformer.Constraints.MaxX.Value,
                translateTransformer.Constraints.MaxY.Value,
                translateTransformer.Constraints.MaxZ.Value);

            initialPosition = transform.position - minOffset;
            finalPosition = transform.position + maxOffset;

            Vector3 offset = initialPosition + (maxOffset / 2);
            transform.position = offset;
        }
    }

     
    private void Update()
    {        
        float normalizedValue = NormalizedDistanceToEndpoint();

        mixer.SetFloat(MixerParameter, Mathf.Log(normalizedValue) * 20);
    }

    private float NormalizedDistanceToEndpoint()
    {
        float range = Vector3.Distance(initialPosition, finalPosition);
        float distanceFromStart = Vector3.Distance(initialPosition, transform.position);

        return Mathf.Clamp(distanceFromStart / range, 0.00001f, 1.0f);
    }
}

using UnityEngine;

public class PlayFromFlag : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float holdTime = 0.25f;

    private float playFrom = -1;
    private float tapOrHoldTimer = 0.0f;

    private void Start()
    {
        // Failsafe to ensure everything is set correctly
        if (Time.timeSinceLevelLoad < 0.1f)
        {
            this.enabled = false;
            tapOrHoldTimer = 0.0f;
            playFrom = -1;
        }
    }

    private void Update()
    {
        // Check or react if its a hold
        tapOrHoldTimer += Time.deltaTime;
        if (tapOrHoldTimer > holdTime)
        {
            playFrom = -1;
        }
    }


    public void OnButtonPressed()
    {
        this.enabled = true;
        
        tapOrHoldTimer = 0.0f;
    }

    public void OnButtonReleased()
    {
        // Check and react if its a tap
        if (tapOrHoldTimer < holdTime)
        {
            if (playFrom < 0)
                playFrom = audioSource.time;
            else
                audioSource.time = playFrom;
        }
        
        this.enabled = false;
    }

}

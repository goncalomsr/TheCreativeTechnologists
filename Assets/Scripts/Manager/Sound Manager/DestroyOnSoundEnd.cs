using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnSoundEnd : MonoBehaviour
{
    private AudioSource audioSource = null;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            DestroyYourself();
        }

        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            DestroyYourself();
        }
    }

    private void DestroyYourself()
    {
        Destroy(gameObject);
    }
}

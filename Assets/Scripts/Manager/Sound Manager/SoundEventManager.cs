using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
class SoundEvent
{
    [SerializeField] public string identifier;
    [SerializeField] public AudioClip audioClip;
    [SerializeField] [Range(0, 1)] public float volume;
}

public class SoundEventManager : MonoBehaviour
{
    #region
    private static SoundEventManager instance = null;
    public static SoundEventManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("SoundEventManager::Instance - No Valid instance");
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    [SerializeField] private GameObject audioPrefab = null;
    [SerializeField] private List<SoundEvent> soundEvents = new List<SoundEvent>();

    public GameObject PlaySound(string id, Vector3 location, bool shouldLoop = false, bool killOnSoundEnd = true)
    {
        SoundEvent respectiveEventToPlay = FindSoundEventByKey(id);
        if (respectiveEventToPlay != null)
        {
            GameObject soundEvent = Instantiate(audioPrefab, location, Quaternion.identity) as GameObject;

            AudioSource audioSource = soundEvent.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.volume = respectiveEventToPlay.volume;
                audioSource.clip = respectiveEventToPlay.audioClip;
                audioSource.loop = shouldLoop;
            }

            if (killOnSoundEnd)
            {
                soundEvent.AddComponent<DestroyOnSoundEnd>();
            }

            return soundEvent;
        }

        Debug.LogError("SoundEventManager::PlaySound: Failed To Find Sound Event");
        return null;
    }

    public GameObject PlaySoundAndAttachTo(string id, GameObject objectToAttachTo, bool shouldLoop = false, bool killOnSoundEnd = true)
    {
        SoundEvent respectiveEventToPlay = FindSoundEventByKey(id);
        if (respectiveEventToPlay != null)
        {
            GameObject soundEvent = Instantiate(audioPrefab, objectToAttachTo.transform.position, objectToAttachTo.transform.rotation) as GameObject;
            soundEvent.transform.parent = objectToAttachTo.transform;

            AudioSource audioSource = soundEvent.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.volume = respectiveEventToPlay.volume;
                audioSource.clip = respectiveEventToPlay.audioClip;
                audioSource.loop = shouldLoop;
            }

            if (killOnSoundEnd)
            {
                soundEvent.AddComponent<DestroyOnSoundEnd>();
            }

            return soundEvent;
        }

        Debug.LogError("SoundEventManager::PlaySoundAndAttachTo: Failed To Find Sound Event" + id);
        return null;
    }


    private SoundEvent FindSoundEventByKey(string id)
    {
        for (int i = 0; i < soundEvents.Count; i++)
        {
            if (soundEvents[i].identifier.CompareTo(id) == 0)
            {
                return soundEvents[i];
            }
        }
        return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RepeatSection : MonoBehaviour
{

    [SerializeField] private float from;
    [SerializeField] private float to;

    [SerializeField] private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (audioSource.time < from)
            audioSource.time = from;
        
        if (audioSource.time > to)
            audioSource.time = from;
    }
}

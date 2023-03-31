using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

// Event for when the timer completes so we can configure dynamically what we need to do
//[System.Serializable] public class TimerCompletedEvent: UnityEvent<string> { }

public class RestartToDemoLevel : MonoBehaviour
{
    [SerializeField] private float timerLength = 420; // 7 minutes

    [SerializeField] private string levelToLoad = "Prototype_V2";

    private float timer = 0.0f;

    private void Start()
    {
        timer = timerLength;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SceneManager.LoadScene(levelToLoad);
        }
    }
}

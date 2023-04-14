using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("shift");
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("q");
                SceneManager.LoadScene("Main");
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                SceneManager.LoadScene("VirtualReality");
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene("VideoMappingProjection");
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("3DModelling");
            }
        }
    }

    public void ToVMP()
    {
        SceneManager.LoadScene("VideoMappingProjection");
    }

    public void ToVR()
    {
        SceneManager.LoadScene("VirtualReality");
    }

    public void To3D()
    {
        SceneManager.LoadScene("3DModelling");
    }

    public void ToMain()
    {
        SceneManager.LoadScene("Main");
    }
}

// Saves screenshot as PNG file.
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.IO;

public class PNGUploader : MonoBehaviour
{
    // Take a shot immediately
    IEnumerator Start()
    {
        yield return UploadPNG();
    }

    IEnumerator UploadPNG()
    {
        // We should only read the screen buffer after rendering is complete
        yield return new WaitForEndOfFrame();

        // Create a texture the size of the screen, RGB24 format
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        // Read screen contents into the texture
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        // Encode texture into JPG
        byte[] bytes = ImageConversion.EncodeToJPG(tex);
        Object.Destroy(tex);

        // For testing purposes, also write to a file in the project folder
        // File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", bytes);
    }
    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", File.ReadAllBytes(Application.streamingAssetsPath + "/test.jpg"), "imageName.jpg");
        form.AddField("userId", "17ac4c482dcdd");

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:5000/", form);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete! " + www.downloadHandler.text);

        }
    }
}

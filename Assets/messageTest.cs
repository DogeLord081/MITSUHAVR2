using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class MessageSender : MonoBehaviour
{
    private string serverURL = "http://192.168.0.22:8000/";

    IEnumerator Start()
    {
        string message = "Bye from the test script!";

        WWWForm form = new WWWForm();
        form.AddField("message", message);

        using (UnityWebRequest www = UnityWebRequest.Post(serverURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("<color=blue>Message sent successfully!</color>");
            }
            else
            {
                Debug.LogError($"<color=red>Failed to send message. Status code</color>: {www.responseCode}");
            }
        }
    }
}
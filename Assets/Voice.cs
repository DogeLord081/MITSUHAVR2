using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Voice : MonoBehaviour
{
    string serverURL = "http://192.168.0.22:8000";  // Replace with your server IP address or domain
    public string speech;
    public bool isAudioPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckForWavFiles());
    }

    IEnumerator CheckForWavFiles()
    {
        while (isAudioPlaying == false)
        {
            print("Checking for .wav files on server...");
            //yield return new WaitForSeconds(1f); // Adjust the interval as needed

            UnityWebRequest wwwList = UnityWebRequest.Get(serverURL + "/uploaded_files/");
            yield return wwwList.SendWebRequest();

            if (wwwList.result == UnityWebRequest.Result.Success)
            {
                string[] fileList = wwwList.downloadHandler.text.Split('\n');
                foreach (string fil in fileList)
                {
                    //string file = fil.Replace("<li>", "").Replace("</li>", "").ToString();
                    string file = DeleteAfterAndIncluding(fil, "</a>");
                    file = DeleteBeforeAndIncluding(file, "\">");
                    print("Found file: " + file);
                    if (file.Contains(".wav"))
                    {
                        speech = file;
                        print("File ends with .wav");
                        string wavURL = serverURL + "/uploaded_files/" + file.ToString();
                        wavURL = wavURL + "";

                        Debug.Log("Attempting to load remote .wav file: " + wavURL);

                        UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(wavURL, AudioType.WAV);
                        yield return www.SendWebRequest();

                        if (www.result == UnityWebRequest.Result.Success)
                        {
                            Debug.Log("Found and loaded remote .wav file: " + file);

                            // Load audio data from the web request
                            AudioClip audioClip = DownloadHandlerAudioClip.GetContent(www);
                            GetComponent<AudioSource>().clip = audioClip;

                            // Play audio
                            isAudioPlaying = true;
                            GetComponent<AudioSource>().Play();
                            isAudioPlaying = false;

                            // Delete the file on the server after the audio has finished playing
                            StartCoroutine(DeleteFileOnServer(wavURL));
                            isAudioPlaying = false;
                            speech = "";
                        }
                        else
                        {
                            Debug.LogError("Error loading remote .wav file: " + www.error);
                        }
                    }
                }
            }
            else
            {
                Debug.LogError("Failed to get file list from server. Error: " + wwwList.error);
            }
        }
    }

    IEnumerator DeleteFileOnServer(string wavURL)
    {
        while (isAudioPlaying)
        {
            yield return null; // Wait until the audio finishes playing
        }

        UnityWebRequest deleteRequest = UnityWebRequest.Delete(wavURL);
        yield return deleteRequest.SendWebRequest();

        if (deleteRequest.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("File deleted on server.");
            isAudioPlaying = false;
        }
        else
        {
            Debug.LogError("Failed to delete file on server. Error: " + deleteRequest.error);
        }
    }

    static string DeleteAfterAndIncluding(string input, string searchString)
    {
        // Find the index of the search string
        int index = input.IndexOf(searchString);

        // Check if the search string was found
        if (index != -1)
        {
            // Delete everything after and including the search string
            return input.Substring(0, index);
        }
        else
        {
            // If the search string is not found, return the original string
            return input;
        }
    }

    static string DeleteBeforeAndIncluding(string input, string searchString)
    {
        // Find the index of the search string
        int index = input.IndexOf(searchString);

        // Check if the search string was found
        if (index != -1)
        {
            // Delete everything before and including the search string
            return input.Substring(index + searchString.Length);
        }
        else
        {
            // If the search string is not found, return the original string
            return input;
        }
    }
}

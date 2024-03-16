using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animActivate : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    string speech;
    bool isAudioPlaying;
    public int animPlayCount;

    void Start()
    {
        animator = GetComponent<Animator>();
        animPlayCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject voiceGameObject = GameObject.Find("Face");

        if (voiceGameObject != null)
        {
            // Get the Voice component from the GameObject
            Voice voiceComponent = voiceGameObject.GetComponent<Voice>();

            if (voiceComponent != null)
            {
                // Access the speech variable
                string speech = voiceComponent.speech;
                bool isAudioPlaying = voiceComponent.isAudioPlaying;
                if (speech == "wave.wav")
                {
                    if (animPlayCount <2)
                    {
                        Debug.Log(speech);
                        //animator.SetTrigger("Wave");
                        animator.SetBool("Wave", true);
                        animPlayCount++;
                    }
                    else
                    {
                        //animator.SetTrigger("Idle");
                        if (isAudioPlaying == false)
                        {
                            animPlayCount = 0;
                        }
                        animator.SetBool("Wave", false);
                        //animPlayCount = 0;
                    }
                }
                else if (speech == "clap.wav")
                {
                    if (animPlayCount < 2)
                    {
                        Debug.Log(speech);
                        //animator.SetTrigger("Clap");
                        animator.SetBool("Clap", true);
                        animPlayCount++;
                    }
                    else
                    {
                        //animator.SetTrigger("Idle");
                        if (isAudioPlaying == false)
                        {
                            animPlayCount = 0;
                        }
                        animator.SetBool("Clap", false);
                        //animPlayCount = 0;
                    }
                }
                else if (speech == "nodding.wav")
                {
                    if (animPlayCount < 2)
                    {
                        Debug.Log(speech);
                        //animator.SetTrigger("Nodding");
                        animator.SetBool("Nodding", true);
                        animPlayCount++;
                    }
                    else
                    {
                        //animator.SetTrigger("Idle");
                        if (isAudioPlaying == false)
                        {
                            animPlayCount = 0;
                        }
                        animator.SetBool("Nodding", false);
                        //animPlayCount = 0;
                    }
                }
                else if (speech == "shaking head.wav")
                {
                    if (animPlayCount < 2)
                    {
                        Debug.Log(speech);
                        //animator.SetTrigger("Shake");
                        animator.SetBool("Shaking head", true);
                        animPlayCount++;
                    }
                    else
                    {
                        //animator.SetTrigger("Idle");
                        if (isAudioPlaying == false)
                        {
                            animPlayCount = 0;
                        }
                        animator.SetBool("Shaking head", false);
                        //animPlayCount = 0;
                    }
                }
                else
                {
                    if (isAudioPlaying == false)
                    {
                        animPlayCount = 0;
                    }
                    animator.SetBool("Wave", false);
                    animator.SetBool("Clap", false);
                    animator.SetBool("Nodding", false);
                    animator.SetBool("Shaking head", false);
                }
                /*else
                {
                    Debug.LogError("Voice component not found on the GameObject.");
                }*/
                /*
                if (speech == "wave.wav" && animPlayCount < 2)
                {
                    Debug.Log(speech);
                    //animator.SetTrigger("Wave");
                    animator.SetBool("Wave", true);
                    animPlayCount++;
                }
                else
                {
                    //animator.SetTrigger("Idle");
                    if (isAudioPlaying == false)
                    {
                        animPlayCount = 0;
                    }
                    animator.SetBool("Wave", false);
                    //animPlayCount = 0;
                }
                */
            } 
            else
            {
                Debug.LogError("Voice component not found on the GameObject.");
            }
        }
        else
        {
            Debug.LogError("Voice GameObject not found.");
        }
    }
}
